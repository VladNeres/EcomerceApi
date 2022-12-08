using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Data.Dto.DtoCategoria
{
    public class UpdateCategoriaDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo nome excedeu a quantidade maxima de 50 caracteres")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú ' '\s]{1,20}", ErrorMessage = "o Campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        public bool Status { get; set; }
        
    }
}
