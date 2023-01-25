using System;
using System.ComponentModel.DataAnnotations;
using UsuariosApi.Models;

namespace UsuariosApi.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required(ErrorMessage ="O campor CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo username é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "o campo e-mail é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage= "O campo password é obrigatório")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage= "O campo repassword é obrigatório para confirmar a senha")]
        [Compare("Password", ErrorMessage ="Os campos password e repassword estão divergentes")]
        public string Repassword { get; set; }

        [Required(ErrorMessage ="O campo data de nascimento é obrigatório")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo Cep é obrigatório")]
        public string CEP { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }


    }
}
