using CategoriaApi.Data.Dto;
using CategoriaApi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Data.Dto.DtoCategoria
{
    public class ReadCategoriaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú ' '\s]{1,20}", ErrorMessage= "o Campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        [Required]
        public bool Status { get; set; }
        public string DataCriacao { get; set; }
        public string DataAtualizacao { get; set; }
        
        public object SubCategoria { get; set; }


    }
}

