using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Model;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using CategoriaApi.Exceptions;
using CategoriaApi.Repository;

namespace CategoriaApi.Services
{
    public class SubCategoriaService
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        private SubCategoriaRepository _subRepository;
        private readonly IDbConnection _dbConnection;
        public SubCategoriaService(DatabaseContext context, IMapper mapper, IDbConnection dbConnection, SubCategoriaRepository repository)
        {
            _subRepository= repository;
            _context= context;
            _mapper = mapper;
            _dbConnection= dbConnection;
        }

        public ReadSubCategoriaDto CriarSubCategoria(CreateSubCategoriaDto subCategoriaDto)
        {
            SubCategoria subCategoriaNome = _subRepository.VerificaSeExistePeloNome(subCategoriaDto);
            Categoria subId = _subRepository.RecuperaCategoriaPorId(subCategoriaDto);

            if(subId == null || subId.Status== false )
            {
                throw new InativeObjectException();
            }
            if (subCategoriaDto.Nome.Length >= 3 && subCategoriaDto.Nome.Length <= 50)
            {
                if (subCategoriaNome == null)
                {

                    SubCategoria subCategoria = _mapper.Map<SubCategoria>(subCategoriaDto);
                    subCategoria.Status = true;
                    subCategoria.DataCriacao = DateTime.Now;
                   _subRepository.AddSubCategoria(subCategoria);
                    return _mapper.Map<ReadSubCategoriaDto>(subCategoria);
                }
                throw  new AlreadyExistException("A já existe uma subCategoria com esse nome");
            }
            throw new MinCharacterException("A categoria deve conter entre 3 e 50 caracteres");
        }

        public Result EditarSubCategoria(int id, UpdateSubCategoriaDto subDto)
        {
            SubCategoria subCategoria = _subRepository.RecuperarSubPorId(id);
            if (subCategoria == null)
            {
                return Result.Fail("Subcategoria não encontrada");
            }
            if(subCategoria.Produtos.Count()> 0 && subDto.Status!= true)
            {
                return Result.Fail("Não é possivel desativar essa subcategoria pois existem produtos cadastrados");
            }
                _mapper.Map(subDto, subCategoria);
                subCategoria.DataAtualizacao = DateTime.Now;
                _subRepository.SalvarAlteracoes();
                return Result.Ok();
        }
        public Result DeletarSubCategoria(int id)
        {
            SubCategoria subCategoria = _subRepository.RecuperarSubPorId(id);
            if (subCategoria == null)
            {
                return Result.Fail("Subcategoria não encontrada");
            }
            _subRepository.DeletarSubCat(subCategoria);
            return Result.Ok();
        }


        public List<ReadSubCategoriaDto> GetSubCategoria(string nome, bool? status, string ordem, int quantidadePorPagina)
        {
            List<SubCategoria> subcategorias = _subRepository.RecuperarListaDeSub(); 
            if (subcategorias == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(nome))
            {
                IEnumerable<SubCategoria> query = from subcategoria in subcategorias
                                               orderby subcategoria.Nome ascending
                                               where subcategoria.Nome.ToUpper().StartsWith(nome.ToUpper())
                                               select subcategoria;

                subcategorias = query.ToList();

            }
            if (status == true || status == false)
            {
                IEnumerable<SubCategoria> query = from subcategoria in subcategorias
                                               where subcategoria.Status == status
                                               select subcategoria;
                subcategorias = query.ToList();
            }
            if (quantidadePorPagina > 0)
            {
                IEnumerable<SubCategoria> query = from subcategoria in subcategorias.Take(quantidadePorPagina)
                                               select subcategoria;
                subcategorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "CRESCENTE")
            {
                IEnumerable<SubCategoria> query = from subcategoria in subcategorias
                                               orderby subcategoria.Nome ascending
                                               select subcategoria;
                subcategorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "DECRESCENTE")
            {
                IEnumerable<SubCategoria> querydecres = from subcategoria in subcategorias
                                                     orderby subcategoria.Nome descending
                                                     select subcategoria;
                subcategorias = querydecres.ToList();
            }

            List<ReadSubCategoriaDto> readDto = _mapper.Map<List<ReadSubCategoriaDto>>(subcategorias);
            return readDto;


        }

        public ReadSubCategoriaDto GetSubPorId(int id)
        {
            SubCategoria subCategoria = _context.SubCategorias.FirstOrDefault(subCategoria => subCategoria.Id == id);
            if (subCategoria != null)
            {
                ReadSubCategoriaDto readSubCategoria = _mapper.Map<ReadSubCategoriaDto>(subCategoria);
                return readSubCategoria;
            }
            return null;
        }
    }
}
