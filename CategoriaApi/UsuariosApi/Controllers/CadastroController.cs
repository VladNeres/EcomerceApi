using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Exceptions;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController: ControllerBase
    {
        private CadastroService _cadastroService;
        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]

        public async Task< IActionResult> CadastraUsuarioPadrao(CreateUsuarioDto usuarioDto)
        {
            try
            {
                Result resultado = await _cadastroService.CadastroUsuarioPadrao(usuarioDto);
                    if (resultado.IsFailed) return StatusCode(500);
                return Ok(resultado.Successes);
            }
            catch (AlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (FormatException e )
            {
                return BadRequest(e.Message);
            }
            catch (NullException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/cadastro-admin")]
        public async Task<IActionResult> CadastrarUsuarioAdmin(CreateUsuarioDto usuariodto)
        {
            try
            {
                Result resultado = await _cadastroService.CadastroUsuarioAdmin(usuariodto);
                if (resultado.IsFailed) return StatusCode(500);
                return Ok(resultado.Successes);
            }
            catch (AlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch(NullException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/ativa")]
        public IActionResult AtivaCadastroUsuario(AtivaContaRequest request)
        {
            try
            {
                Result resultado = _cadastroService.AtivaContaUsuario(request);
                if(resultado.IsFailed) return StatusCode(500);
                return Ok(resultado.Successes);

            }
            catch(NullException e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
