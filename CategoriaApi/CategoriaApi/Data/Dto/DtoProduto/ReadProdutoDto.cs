using CategoriaApi.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Data.Dto.DtoProduto
{
    public class ReadProdutoDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo valor é obrigatório")]
        public string Descricao { get; set; } 
        [Required(ErrorMessage = "O campo peso é obrigatório")]
        public double Valor { get; set; } = 0;
        [Required(ErrorMessage = "O campo quantidade em estoque é obrigatório")]
        public int QuantidadeEmEstoque { get; set; } = 0;
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public double Peso { get; set; } = 0;
        [Required(ErrorMessage = "O campo altura é obrigatório")]
        public double Altura { get; set; } = 0;
        [Required(ErrorMessage = "O campo largura é obrigatório")]
        public double Largura { get; set; } = 0;
        [Required(ErrorMessage = "O campo comprimento é obrigatório")]
        public double Comprimento { get; set; } = 0;
        [Required(ErrorMessage = "O campo subcategoriaId é obrigatório")]
        public int SubCategoriaId { get; set; }
        public int CategoriaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualiazacao { get; set; }
        
       
    }
}
