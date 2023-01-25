using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;
        public LogoutController(LogoutService logoutservice)
        {
            _logoutService = logoutservice;
        }

        [HttpPost]
        public IActionResult DeslogaUsuario()
        {
          Result resultado=  _logoutService.DeslogaUsuario();
            if (resultado.IsFailed) return Unauthorized(resultado.IsFailed);
            return Ok(resultado.Successes);
        }  
    }
}
