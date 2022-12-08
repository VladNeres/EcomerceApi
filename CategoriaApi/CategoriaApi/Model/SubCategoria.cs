using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Model
{
    public class SubCategoria
    {
        [Key]
        [Required]
       public int Id { get; set; }
        [Required(ErrorMessage ="O campo nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O Campo nome excedeu o limite de 50 caracteres ")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '\s]{1,50}", ErrorMessage=  "O campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        public bool Status { get; set; } 
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
 
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }
        [JsonIgnore]
        public virtual List<Produto> Produtos { get;set; }

        
    }
}
