using AutoMapper;
using CategoriaApi.Model;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;

namespace UsuariosApi.Services
{
    public class AtualizaCadastroService
    {
        private CadastroService _cadastroService;
        private UserManager<CustomIdentityUser> _userManager;
        private IMapper _mapper;
        public AtualizaCadastroService(CadastroService cadastroService,UserManager<CustomIdentityUser> manager, IMapper mapper)
        {
            _userManager = manager;
            _mapper = mapper;
            _cadastroService = cadastroService;
        }

        public async Task<Result> AtualizarCadastro(int id,UpdateUsuarioDto updateUserDto)
        {
            CustomIdentityUser username = _userManager.Users.FirstOrDefault(usuario => usuario.Id == id);

            if(username == null)
            {
                return Result.Fail("Usuario não encontrado");
            }

            var cpfValido=   _cadastroService.VerificaCPF(updateUserDto.CPF);
            var endereco = await  _cadastroService.ViaCep(updateUserDto.CEP);
            updateUserDto.UF = endereco.UF;
            updateUserDto.Localidade = endereco.Localidade;
            updateUserDto.Bairro = endereco.Bairro;
            updateUserDto.Logradouro = endereco.Logradouro;
            updateUserDto.Numero = endereco.Numero;
            updateUserDto.Complemento = endereco.Complemento;
            updateUserDto.DataAtualizacao = System.DateTime.Now;
            _mapper.Map(updateUserDto,username);
            await _userManager.UpdateAsync(username);
            return Result.Ok();

        }
    }
}
