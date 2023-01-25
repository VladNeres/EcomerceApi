using Microsoft.AspNetCore.Identity;
using System;

namespace CategoriaApi.Model
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime DataNascimento { get; set; }

    }
}
