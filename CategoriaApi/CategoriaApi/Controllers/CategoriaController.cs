

using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoCategoria;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Services;
using FluentResults;
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
        public IActionResult AdicionarCategoria([FromBody] CreateCategoriaDto categoriaDto)
        {

            try
            {
                ReadCategoriaDto readCategoria = _categoriaServices.AdicionarCategoria(categoriaDto);
                return CreatedAtAction(nameof(GetCategoriaPorId), new { id = readCategoria.Id }, readCategoria);
            }
            catch (AlreadyExistException)
            {
                return BadRequest("A categoria ja existe");
            }
            catch (MinCharacterException)
            {
                return BadRequest("É necessario informar de 3 a 50 caracteres");
            }
            catch(InativeObjectException e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpPut("{id}")]
        public IActionResult EditarCategoria(int id, [FromBody] UpdateCategoriaDto categoriaUpdateDto)
        {
           Result resultado=_categoriaServices.EditarCategoria(id, categoriaUpdateDto);
            if (resultado.IsFailed) return NotFound();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            Result result = _categoriaServices.DeletarCategoria(id);
            if (result.IsFailed) return NotFound();
            return NoContent();
        }


        [HttpGet]
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
