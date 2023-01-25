using AutoMapper;
using CategoriaApi.Data.Dto.DtoCategoria;
using CategoriaApi.Exceptions;
using CategoriaApi.Interfaces;
using CategoriaApi.Model;
using CategoriaApi.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestesUniarios.TestesCategoria
{
    public class CategoriaTestes
    {

       private readonly IMapper _mapperUpdate;
       private readonly IMapper _mapper;
       private readonly IMapper _mapperInterno;
        private readonly ICategoriaRepository _repository;
        private readonly ICategoriaService _service;
        
        public CategoriaTestes()
        {
            
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Categoria, ReadCategoriaDto>();
            });
            var configUpdate = new MapperConfiguration(cfg => {
                cfg.CreateMap< UpdateCategoriaDto, Categoria>();
            });

            _mapperUpdate = configUpdate.CreateMapper();
            _mapperInterno = config.CreateMapper();
            _repository = Substitute.For<ICategoriaRepository>(null);
            _mapper = Substitute.For<IMapper>();
            _service= new CategoriaServices(_mapperInterno,_repository);
        }
        public void VerificaQuantidadeDeCaracteres_MinimoDeCaracteresNaoAtingido_LancaExcecao()
        {
            //Arrange
            var createdto = new CreateCategoriaDto();
            createdto.Nome = "j";

            //ACT
            var categoriaTeste = Assert.Throws<MinCharacterException>(
               ()=> _service.AdicionarCategoria(createdto));

            //ASSERT
            Assert.Equal("É necessario informar de 3 a 50 caracteres", categoriaTeste.Message);
        }

        [Fact]
        public void VerificarCategoriaPorNome_SeACategoriaJaExisteSeExiste_LancaExcecao()
        {
            //Arrange
            var createdto = new CreateCategoriaDto();
            createdto.Nome = "joao";
           
            
            Categoria nomeObjeto= new Categoria();
            nomeObjeto.Nome = "joao";
            nomeObjeto.DataCriacao = DateTime.Now;
            _repository.BuscarNomeCategoria(Arg.Any<CreateCategoriaDto>())
                .Returns(nomeObjeto);
       
            //ACT

           var categoriaTeste=  Assert.Throws<AlreadyExistException>(
               ()=>_service.AdicionarCategoria(createdto));
           
            //ASSERT
            Assert.Equal("A categoria já existe", categoriaTeste.Message);
        }


        [Fact]
        public void IdInformadoForInvalido_NaoAtualizaCategoria()
        {
            //Arrange
            
            UpdateCategoriaDto updateDto = new UpdateCategoriaDto();
            updateDto.Nome = "Novo nome";
            updateDto.Status = true;
                       
            //Act
            var respostaIdInvalido=  _service.EditarCategoria(2,updateDto);
            //Assert
            Assert.True(respostaIdInvalido.IsFailed);
        }

        [Fact]
        public void ContemSubCategoriaCadastrada_AtualizarComStatusFalsoETiverCategoriaCadastrada_LancarExcecao()
        {
          //Arrange
            int id = 1;
            Categoria categoria = new Categoria();
            categoria.Nome = "Joana";
            categoria.Id = 1;
            categoria.Status = true;
            categoria.SubCategoria = new List<SubCategoria>()
            {
                new SubCategoria()
                {
                    Nome= "sub1",
                    Id = 1,
                    Status = true,
                    CategoriaId = id
                }
            };
            _repository.BuscarCategoriaPorId(Arg.Any<int>()).Returns(categoria);
            UpdateCategoriaDto categoriaDto = new UpdateCategoriaDto();
            categoriaDto.Nome = "update1";
            categoriaDto.Status = false;

            //Act
            var excecaoMessage = Assert.Throws<InativeObjectException>( ()=>
                _service.EditarCategoria(id, categoriaDto));

            Assert.Equal("Não é possivel inativar uma categoria que contenha uma subCategoria cadastrada", excecaoMessage.Message);
        }

        [Fact ]
        public void AtualizarCategoria_IdDeCategoriaEStatusValido_AtualizaCategoria()
        {
            //Arrenge
            int id = 1;
            Categoria categoria = new Categoria();
            categoria.Nome = "nome";
            categoria.Id=1;
            categoria.Status = true;
            categoria.SubCategoria = new List<SubCategoria>()
            {
                new SubCategoria()
                {
                    Nome="Sub1",
                    Status=true,
                    DataCriacao=DateTime.Now,
                    CategoriaId=id
                }
            };
            UpdateCategoriaDto updateDto = new UpdateCategoriaDto();
            updateDto.Nome = "Novo nome";
            updateDto.Status = true;
            _repository.BuscarCategoriaPorId(Arg.Any<int>()).Returns(categoria);
           var camposMapeados=  _mapperUpdate.Map(updateDto,categoria);
            _mapper.Map<Categoria>(camposMapeados).Returns(camposMapeados);
                   
            //Action
       
            var testarNovoNome = _service.EditarCategoria(id, updateDto);
            //Assert

            Assert.True(testarNovoNome.IsSuccess);
        }


        [Fact]
        public void ExclusaoInvalidaCategoria_returnFailed()
        {
            //Arrange
            
            Categoria categoria = new Categoria();
            categoria.Nome = "cat1";
            //Act
            var retornoResultFailed = _service.DeletarCategoria(categoria.Id);
            //Assert
            Assert.True(retornoResultFailed.IsFailed);
        }
        [Fact]
        public void ExclusaovalidaCategoria_RetornaOk()
        {
            //Arrange
           
            Categoria categoria = new Categoria();
            categoria.Id = 1;
            categoria.Nome = "cat1";
            _repository.BuscarCategoriaPorId(Arg.Any<int>()).Returns(categoria);
            //Act
            var resultado = _service.DeletarCategoria(categoria.Id);
            //Assert
            Assert.True(resultado.IsSuccess);
        }

        [Fact]
        public void PesquisaDaListaDeCategoria_CampoForDiferenteDeNulo_RetornarListaComCampoSolicitado()
        {
            //Arrenge
            string nome = null;
            bool? status= true;  
            int quantidadePorPagina= 0;
            string ordem = null;

            List<Categoria> listaCategoria = new List<Categoria>()
            {
                new Categoria()
                {
                    Id= 1,
                    Nome= "padaria",
                    DataCriacao= DateTime.Now,
                    Status= true
                },
                new Categoria()
                {
                    Id= 2,
                    Nome= "Uvas",
                    DataCriacao= DateTime.Now,
                    Status= true
                },
                new Categoria()
                {
                    Id= 3,
                    Nome= "Mangas",
                    DataCriacao= DateTime.Now,
                    Status= false
                }

            };

             _repository.BuscarListaCategorias().Returns(listaCategoria);
            var camposMapeados = _mapperInterno.Map<List<ReadCategoriaDto>>(listaCategoria);
            _mapper.Map<List<ReadCategoriaDto>>(listaCategoria).Returns(camposMapeados);
            //Act
            var listaDeNome = _service.GetCategoria(nome, status, quantidadePorPagina, ordem);

            Assert.NotNull(listaDeNome);
        }
    }
}
