using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using scheduler.core.Auth;
using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Entities;
using scheduler.core.Interfaces;
using scheduler.core.Mappings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<User> userManager, JwtSettings jwtSettings, IConfiguration configuration, IUserRepository repository)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _configuration = configuration;
            _repository = repository;
        }

        public List<GetUserResponseDto> GetAll()
        {
            var result = _repository.GetAll();

            var response = UserMapping.FromEntityToDtoList(result);

            return response.ToList();
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
            var pepe = new Random().Next(1, 1000);
            dto.Email = $"claudio.dpedalino{pepe}@gmail.com";
            dto.Password = "Temporal1#";

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return ValidateUserException(AuthValidationErrorResponses.UserAlreadyExist);

            var newUser = new User
            {
                Email = dto.Email,
                UserName = dto.Email,
                RolId = 100
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
            try
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
                    //new Claim("id", newUser.Id)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                return default;
            }
        }

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
