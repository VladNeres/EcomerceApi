using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Controllers
{
        [ApiController]
        [Route("[controller]")]

    public class SubCategoriaController: ControllerBase
    {
        public SubCategoriaService _service;
        public SubCategoriaController (SubCategoriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CriarSubCategoria([FromBody] CreateSubCategoriaDto subCategoriaDto)
        {
            try
            {
                ReadSubCategoriaDto subDto = _service.CriarSubCategoria(subCategoriaDto);
                return CreatedAtAction(nameof(GetSubCategoriaPorId), new { id = subDto.Id }, subDto);

            }
            catch (AlreadyExistException e)
            {
                return BadRequest(e.Message);
            }
            catch (MinCharacterException e)
            {
                return BadRequest(e.Message);
            }
            catch (InativeObjectException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarSubCategoria(int id, [FromBody] UpdateSubCategoriaDto subCategoriaDto) 
        {
            Result sub= _service.EditarSubCategoria(id, subCategoriaDto);
            if (sub.IsFailed) return NotFound("Não é possivel alterar essa sub Categoria pois existem produtos cadastrados"); 
            
                return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarSubCategoria(int id)
        {
            Result result = _service.DeletarSubCategoria(id);
            if(result.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpGet]
        public List<ReadSubCategoriaDto> GetSubCategorias([FromQuery] string nome, [FromQuery] bool? status, [FromQuery] string ordem, [FromQuery] int quantidadePorPagina )
        {
            
            return _service.GetSubCategoria(nome,status,ordem, quantidadePorPagina);
        }

        [HttpGet("{id}")]

        public IActionResult GetSubCategoriaPorId(int id)
        {
            ReadSubCategoriaDto readSub = _service.GetSubPorId(id);
            if(readSub!= null) return Ok(readSub);
            return NotFound();
        }
    }
}
