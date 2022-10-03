﻿using scheduler.core.Auth;
using scheduler.core.Dtos.Requests;
using scheduler.core.Interfaces;
using scheduler.core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using scheduler.core.Respositories;
using AutoMapper;
using scheduler.core.Dtos.Responses;

namespace scheduler.core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public IdentityService(UserManager<User> userManager, JwtSettings jwtSettings,IConfiguration configuration, IUserRepository repository, IMapper mapper)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        public List<GetUserResponseDto> GetAll()
        {
            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetUserResponseDto>>(result);

            return response;
        }

        public async Task<AuthenticationResult> LoginAsync(UserLoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return ValidateUserException(AuthValidationErrorResponses.UserDoesNotExist);

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!userHasValidPassword)
                return ValidateUserException(AuthValidationErrorResponses.UserOrPasswordAreIncorrect);

            return GenerateAuthResult(user);
        }


        public async Task<AuthenticationResult> RegisterAsync(UserRegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return ValidateUserException(AuthValidationErrorResponses.UserAlreadyExist);

            var newUser = new User
            {
                Email = dto.Email,
                UserName = dto.Email,
                IsAdmin = ValidateIsAdmin(dto)
            };

            var createdUser = await _userManager.CreateAsync(newUser, dto.Password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = createdUser.Errors.Select(x => x.Description)
                };
            }

            return GenerateAuthResult(newUser);
        }



        private AuthenticationResult GenerateAuthResult(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.UtcNow.ToString("d")),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }

        private bool ValidateIsAdmin(UserRegisterDto request)
            => request.AdminKey != null
                && _configuration.GetValue<string>("AdminKey").Equals(request.AdminKey);
        private AuthenticationResult ValidateUserException(string validationMessage)
            => new AuthenticationResult
            {
                ErrorMessages = new[] { validationMessage }
            };
    }

    public static class AuthValidationErrorResponses
    {
        public const string UserAlreadyExist = "User with this email address already exists";
        public const string UserOrPasswordAreIncorrect = "User or passwords are incorrect";
        public const string UserDoesNotExist = "User or passwords are incorrect";
    }
}
