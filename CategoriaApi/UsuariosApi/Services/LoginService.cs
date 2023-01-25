using CategoriaApi.Model;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosApi.Data.Requests;
using UsuariosApi.Exceptions;
using UsuariosApi.Models;
using UsuariosApi.Requests;

namespace UsuariosApi.Services
{
    public class LoginService
    {

        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;
        private UserManager<CustomIdentityUser> _userManager;
        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService, UserManager<CustomIdentityUser> userManager)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public Result LogarUsuario(LoginRequest request)
        {
            try
            {
                var usuarioEmail = _signInManager.UserManager.FindByEmailAsync(request.Email);
                var resultadoIdentity = _signInManager
                    .PasswordSignInAsync(usuarioEmail.Result.UserName, request.Password, false, false);
                if (resultadoIdentity.Result.IsNotAllowed)
                {
                    var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioEmail.Result).Result;
                   return Result.Fail("Email não foi confirmado por favor confirme o token -> " + code);
                   
                }
                if (resultadoIdentity.Result.Succeeded)
                {
                    var identityUser = _signInManager.UserManager.Users.
                        FirstOrDefault(usuario => usuario.NormalizedUserName == usuarioEmail.Result.UserName.ToUpper()); ;
                        
                    Token token =_tokenService.CreateToken(identityUser, _signInManager.UserManager
                            .GetRolesAsync(identityUser).Result.FirstOrDefault());
                        return Result.Ok().WithSuccess(token.Value);
                }
                return Result.Fail("Login ou senha incorreto");
            }
            catch (NullReferenceException)
            {
                return Result.Fail("Email não cadastrado");
            }
        }


        public Result DeslogaUsuario()
        {
            var usuarioIdentity = _signInManager.SignOutAsync();
            if (usuarioIdentity.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Logout falhou");
        }


        public Result ReseteSenhaUsuario(EfetuaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPeloEmail(request.Email);
            if (identityUser == null) throw new NullException("Email inexistente"); 
            IdentityResult resultadoIdentity = _signInManager.UserManager
                .ResetPasswordAsync(identityUser, request.Token, request.Password).Result;

            if (resultadoIdentity.Succeeded) return Result.Ok().WithSuccess("Senha alterada com sucesso");
            return Result.Fail("Falha ao solicitar a troca de senha");
        }

        public Result SolicitaReseteSenhaUsuario(SolicitaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPeloEmail(request.Email);

            if(identityUser!= null)
            {
                string codigoDeRecuperacao = _signInManager.UserManager
                    .GeneratePasswordResetTokenAsync(identityUser).Result;

                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }
            return Result.Fail("Falha ao solicitar redefinição da senha");
        }
        public CustomIdentityUser RecuperaUsuarioPeloEmail(string email)
        {
            return _signInManager.UserManager.Users
                            .FirstOrDefault(usuario => usuario.NormalizedEmail == email.ToUpper());
        }
    }
}
