using CategoriaApi.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Data.Dto.DtoCategoria
{
    public class CreateCategoriaDto
    {
        [Required(ErrorMessage = "O Campo nome é obrigatorio")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '/s]{1,1000}", ErrorMessage = "O campo nome deve conter apenas letras")]
        [StringLength(50, ErrorMessage ="Tamanho maximo de 50 caracteres excedido ")]
        public string Nome { get; set; }

        

    }
}
