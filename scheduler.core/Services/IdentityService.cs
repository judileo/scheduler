using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using scheduler.core.Auth;
using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Entities;
using scheduler.core.Enums;
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
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return ValidateUserException(AuthValidationErrorResponses.UserAlreadyExist);

            var newUser = new User
            {
                Email = dto.Email,
                UserName = dto.Email,
                //RolId = (int?)UserRolEnum.Student, // TODO: Modificar para que permita crear un usuario con otro rol ((validar que el rol exista antes)) )
                RolId = dto.RolId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                State = "Active", // Luego podemos agregar lógica para manejar estados de un usuario
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



        private AuthenticationResult GenerateAuthResult(IdentityUser newUser) // TODO: Esto se puede llevar a un servicio aparte para que la clase quede más chica
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

        private AuthenticationResult ValidateUserException(string validationMessage)
            => new AuthenticationResult
            {
                ErrorMessages = new[] { validationMessage }
            };
    }


}
