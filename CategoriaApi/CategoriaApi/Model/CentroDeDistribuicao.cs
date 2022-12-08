using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Model
{
    public class CentroDeDistribuicao
    {
        [Key]
        [Required]
         public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' '/s]{1,10000}", ErrorMessage = "O campo não permite caracteres especiais")]
        [StringLength(128, ErrorMessage = "Tamanho máximo de 128 caracteres excedido ")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo logradouro é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' '/s]{1,10000}", ErrorMessage = "não é permitido utilizar caracteres especiais no campo logradouro")]
        [StringLength(256, ErrorMessage = "Tamanho máximo de 256 caracteres excedido")]
        public string Logradouro { get; set; }

        [RegularExpression("@[0-9/s]{1,500}", ErrorMessage= "o campo número so aceita valores inteiro")]
        [Required(ErrorMessage ="O campo número é obrigatório")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo bairro é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' ']{1,10000}", ErrorMessage = "não é permitido utilizar caracteres especiais no campo bairro")]
        [StringLength(128, ErrorMessage = "Tamanho máximo de 128 caracteres excedido")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo cidade é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' ']{1,10000}", ErrorMessage = "não é permitido utilizar caracteres especiais no campo cidade")]
        [StringLength(128, ErrorMessage = "Tamanho máximo de 128 caracteres excedido")]
        public string Localidade { get; set; }


        [Required(ErrorMessage = "O campo UF é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' ']{1,10000}", ErrorMessage = "não é permitido utilizar caracteres especiais no campo UF")]
        [StringLength(2, ErrorMessage = "Tamanho máximo de 2 caracteres excedido")]
        public string UF { get; set; }


        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        [StringLength(9, ErrorMessage = "Tamanho máximo de 9 caracteres excedido")]
        public string CEP { get; set; }

        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' ']{1,10000}", ErrorMessage = "não é permitido utilizar caracteres especiais no campo complemento")]
        [StringLength(128, ErrorMessage = "Tamanho máximo de 128 caracteres excedido")]

        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo Status é obrigatório")]
        public bool Status { get; set; }
        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
