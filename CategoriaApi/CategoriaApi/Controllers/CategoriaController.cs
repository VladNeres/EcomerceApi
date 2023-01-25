


using CategoriaApi.Data.Dto.DtoCategoria;
using CategoriaApi.Exceptions;
using CategoriaApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private CategoriaServices _categoriaServices;
        public CategoriaController(CategoriaServices services)
        {
            _categoriaServices = services;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AdicionarCategoria([FromBody] CreateCategoriaDto categoriaDto)
        {

            try
            {
                ReadCategoriaDto readCategoria = _categoriaServices.AdicionarCategoria(categoriaDto);
                return CreatedAtAction(nameof(GetCategoriaPorId), new { id = readCategoria.Id }, readCategoria);
            }
            catch (AlreadyExistException e)
            {
                return BadRequest(e.Message);
            }
            catch (MinCharacterException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarCategoria(int id, [FromBody] UpdateCategoriaDto categoriaUpdateDto)
        {
            try
            {
               Result resultado=_categoriaServices.EditarCategoria(id, categoriaUpdateDto);
                if (resultado.IsFailed) return NotFound();
            
                return NoContent();
            }
            catch (InativeObjectException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            Result result = _categoriaServices.DeletarCategoria(id);
            if (result.IsFailed) return NotFound();
            return NoContent();
        }


        [HttpGet]
        [Authorize(Roles="admin, regular", Policy ="idadeMinima")]
        public List<ReadCategoriaDto> GetCategoria([FromQuery] string nome, [FromQuery] bool? status, [FromQuery] int quantidadePorPagina,
            [FromQuery] string ordem)
        {

            return _categoriaServices.GetCategoria(nome, status, quantidadePorPagina, ordem);

        }


        [HttpGet("{Id}")]
        public IActionResult GetCategoriaPorId(int id)
        {
                ReadCategoriaDto categoria= _categoriaServices.GetCategoriaPorId(id);
            if(categoria!=null) return Ok(categoria);
            return NotFound("Não encontrado");
        }

    }
}
