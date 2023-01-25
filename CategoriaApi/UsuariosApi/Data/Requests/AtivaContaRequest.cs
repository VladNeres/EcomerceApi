using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Requests
{
    public class AtivaContaRequest
    {
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string Email { get; set; }
        [Required]
        public string CodigoDeAtivacao { get; set; }
    }
}
