using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.DtoSubCategoria
{
    public class UpdateSubCategoriaDto
    {
        [Required(ErrorMessage ="O campo nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú ' '\s]{1,20}", ErrorMessage = "o Campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        public bool Status { get; set; }

    }
}
