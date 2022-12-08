using AutoMapper;
using CategoriaApi.Data.Dto.DtoProduto;
using CategoriaApi.Model;
using System;

namespace CategoriaApi.Profiles
{
    public class ProdutoProfile:Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CreateProdutoDto, Produto>();
            CreateMap< UpdateProdutoDto, Produto>();
            CreateMap<Produto, ReadProdutoDto>();
                
                
               
        }
    }
}
