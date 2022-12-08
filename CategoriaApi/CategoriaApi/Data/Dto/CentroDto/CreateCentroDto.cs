using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.CentroDto
{
    public class CreateCentroDto
    {

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' '/s]{1,10000}", ErrorMessage = "O campo não permite caracteres especiais")]
        [StringLength(128, ErrorMessage = "Tamanho máximo de 128 caracteres excedido ")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        [StringLength(9, ErrorMessage = "Tamanho máximo de 9 caracteres excedido")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O campo número é obrigatório")]
        public int Numero { get; set; }

        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' '/s]{1,10000}", ErrorMessage = "O campo complemento não permite caracteres especiais")]
        [StringLength(128, ErrorMessage = "Tamanho máximo de 128 caracteres excedido ")]
        public string Complemento { get; set; }


    }
}
