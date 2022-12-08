using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoCategoria;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Repository;
using Dapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CategoriaApi.Services
{
    public class CategoriaServices
    {
        private CategoriaRepository _repository;
        private IMapper _mapper;

        public CategoriaServices(IMapper mapper, CategoriaRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ReadCategoriaDto AdicionarCategoria(CreateCategoriaDto categoriaDto)
        {
            Categoria categoriaNome = _repository.BuscarNomeCategoria(categoriaDto);

            if (categoriaDto.Nome.Length >= 3)
            {
                if (categoriaNome == null)
                {
                    Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
                    categoria.DataCriacao = DateTime.Now;
                    categoria.Status = true;
                    _repository.AdicionarCategoria(categoria);
                    return _mapper.Map<ReadCategoriaDto>(categoria);

                }
                throw new AlreadyExistException();
            }
            throw new MinCharacterException();
        }

        public Result EditarCategoria(int id, UpdateCategoriaDto categoriaDto)
        {
            Categoria categorias = _repository.BuscarCategoriaPorId(id);
            IEnumerable<SubCategoria> subCategorias = _repository.BuscarSubId(id);

            if (categorias == null)
            {
                return Result.Fail("Categoria não encontrada");
            }
            if (categorias.SubCategoria.Count()>0 && categorias.Status==true)
            {
                throw new InativeObjectException("Não é possivel inativar uma categoria que contenha uma subCategoria cadastrada");
            }
            _mapper.Map(categoriaDto, categorias);
            categorias.DataAtualizacao = DateTime.Now;
            _repository.SalvarAlteraçoes();
            return Result.Ok();
        }

        public Result DeletarCategoria(int id)
        {
            Categoria categoria = _repository.BuscarCategoriaPorId(id);
            if (categoria == null)
            {
                return Result.Fail("Id não encontrado");
            }
            _repository.ExcluirCategoria(categoria);
            return Result.Ok();
        }

        public List<ReadCategoriaDto> GetCategoria(string nome, bool? status, int quantidadePorPagina, string ordem)
        {
            List<Categoria> categorias = _repository.BuscarListaCategorias();
            if (categorias == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(nome))
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               orderby categoria.Nome ascending
                                               where categoria.Nome.ToUpper().StartsWith(nome.ToUpper())
                                               select categoria;

                categorias = query.ToList();

            }
            if (status == true || status == false)
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               where categoria.Status == status
                                               select categoria;
                categorias = query.ToList();
            }
            if (quantidadePorPagina > 0)
            {
                IEnumerable<Categoria> query = from categoria in categorias.Take(quantidadePorPagina)
                                               select categoria;
                categorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "CRESCENTE")
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               orderby categoria.Nome ascending
                                               select categoria;
                categorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "DECRESCENTE")
            {
                IEnumerable<Categoria> querydecres = from categoria in categorias
                                                     orderby categoria.Nome descending
                                                     select categoria;
                categorias = querydecres.ToList();
            }
            
             List<ReadCategoriaDto> readDto = _mapper.Map<List<ReadCategoriaDto>>(categorias);
            return readDto;
        }

        public ReadCategoriaDto GetCategoriaPorId(int id)
        {
            Categoria categoria = _repository.BuscarCategoriaPorId(id);
            if (categoria != null)
            {
                ReadCategoriaDto readDto = _mapper.Map<ReadCategoriaDto>(categoria);
                return readDto;
            }
            return null;
        }
    }
}
