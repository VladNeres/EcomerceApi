
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Model
{
    public class Produto
    {
        [Key]
        [Required(ErrorMessage = "O campo id é obrigatório")]
        public int Id { get; set; }

        [StringLength(128, ErrorMessage = "Voce excedeu o tamnho maximo de 128 caracteres")]
        [RegularExpression(@"[a-zA-Z-á-úÁ-Ú' '/s]{1,1000}", ErrorMessage = "O campo nome deve conter apenas letras")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        
        public string Nome { get; set; }

        [StringLength(512, ErrorMessage = "Voce excedeu o tamanho maximo de 512 caracteres")]
        [RegularExpression(@"[a-zA-Z-á-úÁ-Ú' '/s]{1,1000}", ErrorMessage = "O campo descrição nome deve conter apenas letras")]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]

        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo peso é obrigatório")]
        public double? Peso { get; set; }

        [Required(ErrorMessage = "O campo altura é obrigatório")]
        public double? Altura { get; set; }

        [Required(ErrorMessage = "O campo largura é obrigatório")]
        public double? Largura { get; set; }

        [Required(ErrorMessage = "O campo comprimento é obrigatório")]
        public double? Comprimento { get; set; }

        [Required(ErrorMessage = "O campo valor é obrigatório")]
        public double? Valor { get; set; }

        [Required(ErrorMessage = "O campo quantidade em estoque é obrigatório")]
        public int QuantidadeEmEstoque { get; set; }

        [Required(ErrorMessage = "O campo status é obrigatório")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "O campo centro de distribuição é obrigatório")]

        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int CategoriaId { get; set; }
        public int SubCategoriaId { get; set; }
        [JsonIgnore]
        public virtual SubCategoria Subcategoria { get; set; }
        public int CentroDeDistribuicaoId { get; set; }
        [JsonIgnore]
        public virtual  CentroDeDistribuicao CentrodeDistribuicao { get; set; }
       


    }
}
