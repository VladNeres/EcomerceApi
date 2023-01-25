using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos
{
    public class UpdateUsuarioDto
    {
        [Required(ErrorMessage = "O campor CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo username é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "o campo e-mail é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo data de nascimento é obrigatório")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo Cep é obrigatório")]
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }

        [Required(ErrorMessage = "O campo número é obrigatório")]
        public int Numero { get; set; }
        public string Complemento { get; set; }

        public DateTime DataAtualizacao { get; set; }
    }
}
