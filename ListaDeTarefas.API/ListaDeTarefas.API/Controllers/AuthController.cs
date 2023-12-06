using ListaDeTarefas.API.Interfaces;
using ListaDeTarefas.API.Models;
using ListaDeTarefas.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelacoesComIdentity.DTOs;

namespace ListaDeTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            if (await _authService.Login(user))
            {
                var userInfo = await _userRepository.GetUserByUsername(user.Username);
                var info = new TokenAndUserId
                {
                    TokenString = _authService.GenerateTokenString(user),
                    UserId = userInfo.Id,
                    Username = userInfo.Username
                };
                return Ok(info);
            }
            return BadRequest("Erro ao tentar logar.");
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(User user)
        {
            if (await _authService.RegisterUser(user))
            {
                var ApplicationUser = new ApplicationUser
                {
                    Username = user.Username        
                };

                _userRepository.Add(ApplicationUser);
                await _userRepository.SaveAllAsync();
                return Ok("Usuário cadastrado com sucesso.");
            }
            return BadRequest("Erro, tente novamente.");
        }
    }
}
