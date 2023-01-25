using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PesquisaUsuarioController: ControllerBase
    {
        private PesquisaUsuarioService _service;

        public PesquisaUsuarioController(PesquisaUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<ReadUsuarioDto> PesquisarUsuario([FromQuery]string email, [FromQuery] string cpf,
            [FromQuery] string username,[FromQuery] DateTime? datanascimento,[FromQuery] string cep, [FromQuery]bool? status )
        {
            var result = _service.PesquisarUsuario( email, cpf,username, datanascimento,cep, status);

            return result;
        }
    }
}
