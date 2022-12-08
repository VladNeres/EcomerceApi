using AutoMapper;
using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Model;
using System;
using System.Linq;

namespace CategoriaApi.Profiles
{
    public class ProfileSubCategoria:Profile
    {

        public ProfileSubCategoria()
        {
            CreateMap<CreateSubCategoriaDto, SubCategoria>();
            CreateMap<UpdateSubCategoriaDto, SubCategoria>();
            CreateMap<SubCategoria, ReadSubCategoriaDto>()
                .ForMember(subCategoria => subCategoria.Produtos, opts => opts
                .MapFrom(subCategoria => subCategoria.Produtos.Select(produto => new
                {
                    produto.Id,
                    produto.Nome,
                    produto.Valor,
                    produto.QuantidadeEmEstoque,
                    produto.Status,
                    produto.DataCriacao,
                    produto.DataAtualizacao
                }
                )));
        }
    }
}
