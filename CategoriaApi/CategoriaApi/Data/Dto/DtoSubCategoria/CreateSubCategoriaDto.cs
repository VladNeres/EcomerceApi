using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.DtoSubCategoria
{
    public class CreateSubCategoriaDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '\s]{1,1000}", ErrorMessage = "O campo nome deve conter apenas letras")]
        [StringLength(50, ErrorMessage = "Tamanho maximo de 50 caracteres excedido")]
        public string Nome { get; set; }
     
        [Required(ErrorMessage = "É necessario informar o Id da categoria que deseja cadastar a subcategoria")]
        public int CategoriaId { get; set; }
    }
}
