using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Repository;
using FluentResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CategoriaApi.Services
{
    public class CentroDeDistribuicaoService
    {
        private CentroRepository _repository;
       private IMapper _mapper;
        

        public CentroDeDistribuicaoService( IMapper mapper, CentroRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task <CentroDeDistribuicao> ViaCep(string cep)
        {
                HttpClient client = new HttpClient();

                var requisicao = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                var resposta = await requisicao.Content.ReadAsStringAsync();
                if(!requisicao.IsSuccessStatusCode)
                {
                    throw new NullException();
                }
                    var endereco = JsonConvert.DeserializeObject<CentroDeDistribuicao>(resposta);
                    return endereco;
        }
            
           
     
        public async Task<ReadCentroDto> AddCentroDeDistribuicao(CreateCentroDto centroDto)
        {
           
            CentroDeDistribuicao categoriaNome = _repository.RetornarNomeDocentro(centroDto);
            CentroDeDistribuicao centroEndereco = _repository.RetornarEndereco(centroDto);
            
            if (centroDto.Nome.Length >= 3)
            {
                if (categoriaNome == null)
                {
                    if(centroEndereco!= null && centroEndereco.Numero== centroDto.Numero )
                    {
                         throw new AlreadyExistException("Esse endereço já foi cadastrado");
                    }
                    
                        var endereco= await ViaCep(centroDto.CEP);
                        CentroDeDistribuicao centro = _mapper.Map<CentroDeDistribuicao>(centroDto);
                        centro.DataCriacao = DateTime.Now;
                        centro.Status = true;
                        _repository.AddCentro(centro,endereco);
                        return _mapper.Map<ReadCentroDto>(centro);
                }
                throw new AlreadyExistException("Esse nome de centro de distribuição já existe");
            }
            throw new MinCharacterException();
        }

        public Result AtualizarCentroService(int id, UpdateCentroDto centroDto)
        {
            CentroDeDistribuicao centro = _repository.RecuperarCentroPorId(id);
            if(centro == null)
            {
                return Result.Fail("Centro não encontrado");
            }
            if(centro.Produtos.Count()>0 && centro.Status == true)
            {
                throw new InativeObjectException("Não é possivel inativar um centro que contenha um produto cadastrado");
            }
            var endereço = ViaCep(centroDto.CEP);
            centroDto.CEP = endereço.Result.CEP;
            centroDto.Logradouro = endereço.Result.Logradouro;
            centroDto.Bairro = endereço.Result.Bairro;
            centroDto.Localidade = endereço.Result.Localidade;
            centroDto.UF = endereço.Result.UF;
            CentroDeDistribuicao centroupdate = _mapper.Map(centroDto, centro);
            centroupdate.DataAtualizacao = DateTime.Now;
            _repository.Salvar();
            return Result.Ok();
        }

        public Result ExcluirCentro(int id)
        {
            var centro = _repository.RecuperarCentroPorId(id);

            if (centro == null)
            {
                return Result.Fail("Centro não encontrado");
            }
            _repository.ExcluirCentro(centro);
            return Result.Ok();
        }

        public ReadCentroDto RecuperarCentroPorId(int id)
        {
            CentroDeDistribuicao centro = _repository.RecuperarCentroPorId(id);
            if (centro != null)
            {
                ReadCentroDto centroDto = _mapper.Map<ReadCentroDto>(centro);
                return centroDto;
            }
                return null;

        }

        public List<CentroDeDistribuicao> GetCentroDeDistribuicao(CentroPesquisa pesquisa)

        {
            return _repository.GetCentroDeDistribuicao(pesquisa);
        }
    }
}
