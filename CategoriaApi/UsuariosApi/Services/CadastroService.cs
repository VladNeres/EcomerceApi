using AutoMapper;
using CategoriaApi.Model;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Exceptions;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        public CadastroService(IMapper mapper, UserManager<CustomIdentityUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public bool VerificaCPF(string cpf)
        {
            CPFCNPJ.IMain checkCPF = new CPFCNPJ.Main();
            var resultCPF = checkCPF.IsValidCPFCNPJ(cpf);
            if (resultCPF == false) throw new NullException("CPF Invalido! por favor digite um CPF valido");
            return resultCPF;
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var verificaEmail = new MailAddress(email);
                return verificaEmail.Address.Equals(email);
            }
            catch (FormatException)
            {
                throw new FormatException("Formato de e-mail invalido");
            }
        }

        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            CustomIdentityUser identityUser = RecuperarUsuarioPorEmail(request.Email);
            if (identityUser == null) throw new NullException("Email de confirmação invalido");
            var IdentityResult = _userManager.ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;
            if (IdentityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta de usuario"); 
        }

        public async Task<Usuario> ViaCep(string cep)
        {
            HttpClient client = new HttpClient();
            var requisicao = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var resposta = await requisicao.Content.ReadAsStringAsync();
            if (!requisicao.IsSuccessStatusCode)
            {
                throw new NullException("CEP Invalido por favor digite um CEP valido");
            }
            var endereco = JsonConvert.DeserializeObject<Usuario>(resposta);
            return endereco;
        }

        public async Task<Result> CadastroUsuarioPadrao(CreateUsuarioDto createDto)
        {
            CustomIdentityUser usuarioExiste = RecuperarUsername(createDto.Email);
            CustomIdentityUser emailExtiste= RecuperarUsuarioPorEmail(createDto.Email);
            if (usuarioExiste != null || emailExtiste!=null) throw new AlreadyExistsException("UserName ou email já existe!");

            var emailValido = IsValidEmail(createDto.Email);
            var verificaCpf = VerificaCPF(createDto.CPF);
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            await inserindoResultadoDoCEP(createDto, usuario);

            //mapeando de um Usuario para um Identityuser e criando Role Regular
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);
            var resultadoIdentity =  _userManager.CreateAsync(usuarioIdentity, createDto.Repassword);
            await _userManager.AddToRoleAsync(usuarioIdentity, "regular");

            if (resultadoIdentity.Result.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                return Result.Ok().WithSuccess(code);
            }
            return Result.Fail("Falha ao cadastrar usuario");
        }


        public async Task<Result> CadastroUsuarioAdmin(CreateUsuarioDto createDto)
        {
            var usernameExists = RecuperarUsername(createDto.Email);
            var EmailExists = RecuperarUsuarioPorEmail(createDto.Email);
            if (usernameExists != null || EmailExists !=null) throw new AlreadyExistsException("UserName ou email já existe!");

            bool emailValido = IsValidEmail(createDto.Email);
            bool verificarCpf = VerificaCPF(createDto.CPF);
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            await inserindoResultadoDoCEP(createDto, usuario);

            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);

            var resultIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            var createRoleResult = _userManager.AddToRoleAsync(usuarioIdentity, "admin");

            if (resultIdentity.Result.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                return Result.Ok().WithSuccess(code);
            }
            return Result.Fail("Falha ao Cadastrar usuario");
        }
        private  async Task inserindoResultadoDoCEP(CreateUsuarioDto createDto, Usuario usuario)
        {
                var endereco = await ViaCep(createDto.CEP);
                usuario.Status = true;
                usuario.DataCriacao = DateTime.Now;
                usuario.CEP = createDto.CEP;
                usuario.UF = endereco.UF;
                usuario.Localidade = endereco.Localidade;
                usuario.Bairro= endereco.Bairro;
                usuario.Logradouro = endereco.Logradouro;
                usuario.Numero = createDto.Numero;
                usuario.Complemento = createDto.Complemento;
        }
        private CustomIdentityUser RecuperarUsername(string userName)
        {
            return _userManager.Users.FirstOrDefault(u => u.NormalizedUserName == userName.ToUpper());
        }
        private CustomIdentityUser RecuperarUsuarioPorEmail(string email)
        {
            return _userManager.Users
                .FirstOrDefault(user => user.NormalizedEmail == email.ToUpper());
        }
    }
}
    