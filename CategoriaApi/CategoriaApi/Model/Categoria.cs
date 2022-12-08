using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Model
{
    public class Categoria
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required (ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression( @"[a-zA-Zá-úÁ-Ú' '\s]{1,1000}", ErrorMessage = "O campo nome deve conter apenas letras")]
        [StringLength(50, ErrorMessage ="Tamanho maximo excedido ")]
        public string Nome { get; set; }
        public bool Status { get; set; } 
        public DateTime DataCriacao { get; set; } 
        public DateTime DataAtualizacao{ get; set; }
        [JsonIgnore]
        public virtual List<SubCategoria> SubCategoria { get; set; }

        

    }
}
 ;