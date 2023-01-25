using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtualizarCadastro : ControllerBase
    {
        private AtualizaCadastroService _atualizaService;
        public AtualizarCadastro(AtualizaCadastroService service)
        {
            _atualizaService=service;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCadastroUsuario(int id, UpdateUsuarioDto updateUsuario)
        {
            Result resultado = await _atualizaService.AtualizarCadastro(id, updateUsuario);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }
    }
}
