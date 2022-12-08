using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoProduto;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Repository;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CategoriaApi.Services
{
    public class ProdutoServices
    {
        private IMapper _mapper;
        private ProdutoRepository _produtoRepository;

        public ProdutoServices( IMapper mapper, ProdutoRepository produtoRepos)
        {
            _produtoRepository = produtoRepos;
            _mapper = mapper;
        }


        public ReadProdutoDto AdicionarProduto(CreateProdutoDto produtoDto)
        {

            SubCategoria subId = _produtoRepository.SubCategoriaID(produtoDto);
            Produto produtoNome = _produtoRepository.VerificaSeJaExiste(produtoDto);
            CentroDeDistribuicao centroId = _produtoRepository.CentroDeDistribuicaoID(produtoDto);

            if (produtoDto.Nome.Length<3 || produtoDto.Nome.Length>50)
            {
                throw new MinCharacterException();
            }
            if (subId == null || centroId== null)
            {
                throw new NullException();
            }
            if (subId.Status == false)
            {
                throw new InativeObjectException();
            }
            if (produtoNome == null)
            {
                Produto produto = _mapper.Map<Produto>(produtoDto);
                produto.DataCriacao = DateTime.Now;
                produto.CategoriaId = subId.CategoriaId;
                produto.Status = true;
                _produtoRepository.AddProduto(produto);
                return _mapper.Map<ReadProdutoDto>(produto);

            }
            throw new AlreadyExistException();
        }


        public Result AtualizarProduto(int id, UpdateProdutoDto produtoDto)
        {
            Produto produto = _produtoRepository.RecuperarProdutoPorId(id);
            if (produto == null)
            {
                return Result.Fail("Produto não encontrado");
            }
            _mapper.Map(produtoDto, produto);
            produto.DataAtualizacao = DateTime.Now;
            _produtoRepository.SalvarAlteracoes();
            return Result.Ok();

        }

        public Result DeletarProduto(int id)
        {
            Produto produto = _produtoRepository.RecuperarProdutoPorId(id);
            if (produto == null)
            {
                return Result.Fail("Produto não encontrado");
            }
            _produtoRepository.DeleteAsync(id);
            return Result.Ok();
        }

        public ReadProdutoDto GetProdutoPorId(int id)
        {
            Produto produto = _produtoRepository.RecuperarProdutoPorId(id);
            if (produto != null)
            {
                ReadProdutoDto readDto = _mapper.Map<ReadProdutoDto>(produto);
                return readDto;
            }
            return null;
        } 

    }
}
