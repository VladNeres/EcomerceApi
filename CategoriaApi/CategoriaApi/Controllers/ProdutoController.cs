

using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoProduto;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Repository;
using CategoriaApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private ProdutoRepository _produtoRepository;
        private ProdutoServices _produtoServices;
        public ProdutoController(ProdutoServices services, ProdutoRepository repository)
        {
             _produtoServices = services;
            _produtoRepository = repository;
        }

        [HttpPost]
        public IActionResult AdicionarProduto([FromBody] CreateProdutoDto produtoDto)
        {
            try
            {

                ReadProdutoDto prodServices = _produtoServices.AdicionarProduto(produtoDto);
                return CreatedAtAction(nameof(GetProdutoPorId), new { Id = prodServices.Id }, prodServices);

            }
            catch (NullException)
            {
                return BadRequest("Subcategoria ou Centro de destribuição não encontrado," +
                    " por favor insira uma subcategoria ou centro valido");
            }
            catch (MinCharacterException)
            {
                
                return BadRequest("O produto deve ser criado com no minimo 3 caracteres");

            }
            catch (InativeObjectException)
            {
                return BadRequest("Não é possivel criar um produto em uma subCategoria inativa");
            }
            catch (AlreadyExistException )
            {
                return BadRequest("Já existe um produto com esse nome");
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualiazarProduto(int id, [FromBody] UpdateProdutoDto produtoDto)
        {
           Result readDto = _produtoServices.AtualizarProduto(id, produtoDto);
            if (readDto.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduto( int id)
        {
            Result result = _produtoServices.DeletarProduto(id);
            if(result.IsFailed) return NotFound();
            return NoContent();
        }



        [HttpGet("{id}")]
        public IActionResult GetProdutoPorId(int id)
        {
            ReadProdutoDto readDto = _produtoServices.GetProdutoPorId(id);
            if (readDto != null) return Ok(readDto);
            return NotFound();
        }

        [HttpGet]
        public List<Produto> PesquisaComFiltros([FromQuery] string nome, [FromQuery] bool? status, [FromQuery] double? peso,
            [FromQuery] double? altura, [FromQuery] double? largura, [FromQuery] double? comprimento, [FromQuery] double? valor,
            [FromQuery] int? estoque, [FromQuery] string ordem, [FromQuery] int itensPorPagina, [FromQuery] int paginas)
        {
            return _produtoRepository.PesquisaComFiltros(nome, status, peso, altura, largura, comprimento,
                valor, estoque, ordem, itensPorPagina,paginas);
        }

    }
}
