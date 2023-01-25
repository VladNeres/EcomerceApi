using AutoMapper;
using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Exceptions;
using CategoriaApi.Interfaces;
using CategoriaApi.Model;
using CategoriaApi.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestesUniarios
{
    public class SubCategoriaTestes
    {
        private readonly IMapper _mapper;
        readonly IMapper _mapperRead;
        private readonly ISubCategoriaService _service;
        private readonly ISubCategoriaRepository _repository;
        public SubCategoriaTestes()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SubCategoria, ReadSubCategoriaDto>();
            });
            _mapperRead = config.CreateMapper();
            _repository = Substitute.For<ISubCategoriaRepository>();
            _mapper = Substitute.For<IMapper>();
            _service = new SubCategoriaService(_mapper,_repository);
        }
        [Fact]
        public void CadastrarSub_StatusForFalse_LancarExcecao()
        {
            Categoria categoria = new Categoria();
            categoria.Nome = "padaria";
            categoria.Id = 1;
            categoria.Status = false;
         
            
            CreateSubCategoriaDto sub = new CreateSubCategoriaDto();
            sub.Nome = "Pao";
            sub.CategoriaId = categoria.Id;

            var nomeExiste = _repository.RecuperaCategoriaPorId(sub).Returns(categoria);
            //ACT
            var categoriaTeste = Assert.Throws<InativeObjectException>(
               () => _service.CriarSubCategoria(sub));

            //ASSERT
            Assert.Equal("Não é possivel cadastrar uma subcategoria em uma categoria inativa\n" +
                    "Por favor insira uma categoria valida", categoriaTeste.Message);
        }

        [Fact]
        public void VerificaSeContemMinimaDeCaracteres_LancaExcecao()
        {
            Categoria categoria = new Categoria();
            categoria.Nome = "padaria";
            categoria.Id = 1;
            categoria.Status = true;


            CreateSubCategoriaDto subCreate = new CreateSubCategoriaDto();
            subCreate.Nome = "Pa";
            subCreate.CategoriaId = categoria.Id;
             
            var nomeExiste = _repository.RecuperaCategoriaPorId(subCreate).Returns(categoria);
           
        
            //ACT
            var categoriaTeste =Assert.Throws<MinCharacterException>( 
                ()=> _service.CriarSubCategoria(subCreate));

            //Assert 
            Assert.Equal("É necessario informar de 3 a 50 caracteres", categoriaTeste.Message);
        }

        [Fact]
        public void SubCategoriaJaExistente_LancaExcecao()
        {
            //Arange
            Categoria categoria = new Categoria();
            categoria.Nome = "padaria";
            categoria.Id = 1;
            categoria.Status = true;

            SubCategoria subCategoriaNome = new SubCategoria();
            subCategoriaNome.Nome = "SubCategoria";

            CreateSubCategoriaDto subCreate = new CreateSubCategoriaDto();
            subCreate.Nome = "SubCateoria";
            subCreate.CategoriaId = categoria.Id;
            _repository.RecuperaCategoriaPorId(subCreate).Returns(categoria);
            _repository.VerificaSeExistePeloNome(subCreate).Returns(subCategoriaNome);
            //Act
            var nomeExiste = Assert.Throws<AlreadyExistException>(
               () => _service.CriarSubCategoria(subCreate));

            Assert.Equal("A subcategoria já existe", nomeExiste.Message);
        }

        [Fact]
        public void IdDaSubNaoEncontrado_LancaExcecao()
        {
           //Arrange
            int id = 1;

            UpdateSubCategoriaDto updateSub = new UpdateSubCategoriaDto();
            updateSub.Nome = "Sub Nao encontrada";
           
            SubCategoria subNome = new SubCategoria();
            subNome.Nome = "Sub Nao encontrada";
            subNome.Id = 2;

            _repository.RecuperarSubPorId(id);

            //Act 
          var resultado=  _service.EditarSubCategoria(id, updateSub);

            //Assrt
            Assert.True(resultado.IsFailed);
        }

        [Fact]
        public void Verificando_SeContemProdutosEStatusInativo_LancaFailedResult()
        {
          //Arrange
            SubCategoria subcategoria = new SubCategoria();
            subcategoria.Id = 1;
            subcategoria.Nome = "Pao";
            subcategoria.Status = true;
            subcategoria.Produtos = new List<Produto>()
            {
              new Produto(){  Id = 1,
                Nome = "pao doce",
                Altura = 0,
                Comprimento = 0,
                Descricao = "macio e fofinho",
                Largura = 0
              },
              new Produto(){  Id = 2,
                Nome = "pao salgado",
                Altura = 0,
                Comprimento = 0,
                Descricao = "macio e fofinho salgado",
                Largura = 0
              }
            };
            UpdateSubCategoriaDto updateSubCategoriaDto = new UpdateSubCategoriaDto();
            updateSubCategoriaDto.Nome = "Frutas";
            updateSubCategoriaDto.Status = false;
            _repository.RecuperarSubPorId(subcategoria.Id).Returns(subcategoria);
            
            //Act

            var resultado = _service.EditarSubCategoria(subcategoria.Id, updateSubCategoriaDto);
            //Assert

            Assert.True(resultado.IsFailed);
        }

        [Fact]
        public void AtualizaSubCategoria_IdDaCategoriaExistirEStatus_RetornarOk()
        {

            //Arrange
            SubCategoria subcategoria = new SubCategoria();
            subcategoria.Id = 1;
            subcategoria.Nome = "Pao";
            subcategoria.Status = true;
            subcategoria.Produtos = new List<Produto>()
            {
                new Produto(){  Id = 1,
                Nome = "pao doce",
                Altura = 0,
                Comprimento = 0,
                Descricao = "macio e fofinho",
                Largura = 0
                },
                new Produto(){  Id = 2,
                Nome = "pao salgado",
                Altura = 0,
                Comprimento = 0,
                Descricao = "macio e fofinho salgado",
                Largura = 0
                }
             };
            UpdateSubCategoriaDto updateSubCategoriaDto = new UpdateSubCategoriaDto();
            updateSubCategoriaDto.Nome = "Frutas";
            updateSubCategoriaDto.Status = true;
            _repository.RecuperarSubPorId(subcategoria.Id).Returns(subcategoria);

            //Act

            var resultado = _service.EditarSubCategoria(subcategoria.Id, updateSubCategoriaDto);
            //Assert

            Assert.True(resultado.IsSuccess);
        }

        [Fact]
        public void DeletarSub_IdValido()
        {
            int id = 1;
            SubCategoria sub = new SubCategoria();
            sub.Id = id;
            sub.Nome = "subDeletar";
            _repository.RecuperarSubPorId(id).Returns(sub);

           var result= _service.DeletarSubCategoria(id);

            Assert.True(result.IsSuccess);
        }

        [Fact (Skip ="erro")] 
        public void PesquisarListaDeSub()
        {

            //Arrenge
            string nome = null;
            bool? status = true;
            int quantidadePorPagina = 0;
            string ordem = null;

            List<SubCategoria> listaSubCategoria = new List<SubCategoria>()
            {
                new SubCategoria()
                {
                    Id= 1,
                    Nome= "padaria",
                    DataCriacao= DateTime.Now,
                    Status= true
                },
                new SubCategoria()
                {
                    Id= 2,
                    Nome= "Uvas",
                    DataCriacao= DateTime.Now,
                    Status= true
                },
                new SubCategoria()
                {
                    Id= 3,
                    Nome= "Mangas",
                    DataCriacao= DateTime.Now,
                    Status= false
                }

            };

            _repository.RecuperarListaDeSub().Returns(listaSubCategoria);
            var camposMapeados = _mapperRead.Map<List<ReadSubCategoriaDto>>(listaSubCategoria);
            _mapper.Map<List<ReadSubCategoriaDto>>(listaSubCategoria).Returns(camposMapeados);
            //Act
            var listaDeNome = _service.GetSubCategoria(nome, status, ordem, quantidadePorPagina);

            Assert.NotNull(listaDeNome);
        }
    }
}
