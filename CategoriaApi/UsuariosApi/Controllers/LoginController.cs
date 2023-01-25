using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using UsuariosApi.Data.Requests;
using UsuariosApi.Exceptions;
using UsuariosApi.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult loginUsuario(LoginRequest request)
        {
            try
            {
                Result resultado = _loginService.LogarUsuario(request);
                if (resultado.IsFailed) return Unauthorized(resultado.Errors);
                return Ok(resultado.Successes);
            }
            catch(ArgumentException e)
            {
                return Unauthorized(e.Message);
            }
            catch(AlreadyExistsException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("/deslogar")]
        public IActionResult DeslogaUsuario()
        {
            Result resultado = _loginService.DeslogaUsuario();
            if (resultado.IsFailed) return Unauthorized(resultado.IsFailed);
            return Ok(resultado.Successes);
        }

        [HttpPost("/solicita-reset")]
        public IActionResult SolicitaReseteSenhaUsuario(SolicitaResetRequest request)
        {
            Result resultado = _loginService.SolicitaReseteSenhaUsuario(request);
            if(resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
        }
        
        [HttpPost("/efetua-reset")]
        public IActionResult ReseteSenhaUsuario(EfetuaResetRequest request)
        {
            try
            {
                Result resultado = _loginService.ReseteSenhaUsuario(request);
                if(resultado.IsFailed) return Unauthorized(resultado.Errors);
                return Ok(resultado.Successes);
            }
            catch (NullException e)
            {
                return Unauthorized(e.Message);
            }
        }

    }
}
