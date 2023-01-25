using AutoMapper;
using CategoriaApi.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UsuariosApi.Data.Dtos;


namespace UsuariosApi.Services
{
    public class PesquisaUsuarioService
    {
        private UserManager<CustomIdentityUser> _userManager;
        private IMapper _mapper;
        public PesquisaUsuarioService(UserManager<CustomIdentityUser> usuario, IMapper mapper)
        {
            _userManager = usuario;
            _mapper = mapper;
        }
        public List<ReadUsuarioDto> PesquisarUsuario(string email, string cpf, string username, DateTime? datanascimento,
            string cep,bool? status)
        {

            List<CustomIdentityUser> usuarios = _userManager.Users.ToList();
  
            if (usuarios == null)
            {
                return null;
            }
            if (email!=null)
            {
                IEnumerable<CustomIdentityUser> query = from usuario in usuarios
                                               where usuario.Email.ToUpper().StartsWith(email.ToUpper())
                                               select usuario;

                usuarios = query.ToList();

            }
            if (cpf!=null)
            {
                IEnumerable<CustomIdentityUser> query = from usuario in usuarios
                                                        where usuario.CPF == cpf
                                                        select usuario;
                usuarios= query.ToList();
            }
            if (!string.IsNullOrEmpty(username))
            {
                IEnumerable<CustomIdentityUser> query = from usuario in usuarios
                                                        where usuario.UserName.ToUpper().StartsWith(username.ToUpper())
                                                        select usuario;
                usuarios=query.ToList();
            }
            if (datanascimento != null)
            {
                IEnumerable<CustomIdentityUser> query = from usuario in usuarios
                                                        where usuario.DataNascimento == datanascimento
                                                        select usuario;
                usuarios = query.ToList();
            }
            if (!string.IsNullOrEmpty(cep))
            {
                IEnumerable<CustomIdentityUser> query = from usuario in usuarios
                                                        where usuario.Cep == cep
                                                        select usuario;
                usuarios = query.ToList();
            }
            if(status!= null)
            {
                IEnumerable<CustomIdentityUser> query = from usuario in usuarios
                                                        where usuario.Status == status
                                                        select usuario;
                usuarios = query.ToList();
            }
            List<ReadUsuarioDto> readDto = _mapper.Map<List<ReadUsuarioDto>>(usuarios);
            return readDto;
        }
    }
}
