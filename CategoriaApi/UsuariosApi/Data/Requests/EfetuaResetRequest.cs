using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Requests
{
    public class EfetuaResetRequest
    {
        [Required(ErrorMessage = "O campo password é obrigatório")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo de repassword é obrigatório")]
        [Compare("Password")]
        public string Repassword { get; set; }

        [Required(ErrorMessage ="O campo Email é obrigatório")]
        public string  Email { get; set; }

        [Required(ErrorMessage ="O campo Token é obrigatório")]
        public string Token { get; set; }

    }
}
