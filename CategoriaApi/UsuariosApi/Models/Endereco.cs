using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Models
{
    public class Endereco
    {
        [Required( ErrorMessage = "O campo Cep é obrigatório")]
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }

        [Required(ErrorMessage ="O campo número é obrigatório")]
        public int Numero { get; set; }
        public string Complemento { get; set; }
    }
}
