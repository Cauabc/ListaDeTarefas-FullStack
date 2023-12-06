using ListaDeTarefas.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ListaDeTarefas.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> RegisterUser(User user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.Username,
                PasswordHash = user.Password,
            };
            var result = await _userManager.CreateAsync(identityUser, user.Password);
            return result.Succeeded;
        }

        public async Task<bool> Login(User user)
        {
            var identityUser = await _userManager.FindByNameAsync(user.Username);
            if (identityUser == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public string GenerateTokenString(User user)
        {
            List<Claim> claims = new List<Claim>(){
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "User"),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));

            var signinCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
            claims: claims,
                expires: DateTime.Now.AddHours(1),
                issuer: _configuration.GetSection("Jwt:Issuer").Value!,
                audience: _configuration.GetSection("Jwt:Audience").Value!,
                signingCredentials: signinCred
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
