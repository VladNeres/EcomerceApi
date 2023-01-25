using AutoMapper;
using CategoriaApi.Model;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Profiles
{
    public class UsuarioProfile:Profile
    {

        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
            CreateMap<Usuario, CustomIdentityUser>();
            CreateMap<CustomIdentityUser, ReadUsuarioDto>();
            CreateMap<UpdateUsuarioDto,CustomIdentityUser>();
        }
    }
}
