using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Models
{
    public class Usuario
    {
        [StringLength(250, ErrorMessage ="Voce excedeu o limite de 250 caracteres")]
        [Required(ErrorMessage ="O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Z-á-úÁ-Ú0-9 ' '\s]{1,1000", ErrorMessage = "O campo nome não permite caracteres especiais")]

        [JsonProperty ("Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="O campo CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo Data de nascimento é obrigatório")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        [DataType(DataType.DateTime,ErrorMessage ="Formato invalido")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage ="O campo e-mail é obrigatório")]
        [EmailAddress(ErrorMessage ="Formato de e-mail invalido")]
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "Campo de status obrigatório")]
        public bool Status { get; set; }

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
