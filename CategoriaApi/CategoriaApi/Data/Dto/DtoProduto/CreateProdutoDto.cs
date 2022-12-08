using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.DtoProduto
{
    public class CreateProdutoDto
    {

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '/s]{1,1000}", ErrorMessage = "O campo nome não deve conter numeros ou caracteres especiais")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres excedido")]
        public string Nome { get; set; }
       
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '/s]{1,1000}", ErrorMessage = "O campo descrição não deve conter números ou caracteres especiais")]
        [Required(ErrorMessage = "O Campo descrição é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo peso é obrigatório")]
        public double Peso { get; set; }

        [Required(ErrorMessage = "O campo altura é obrigatório")]
        public double Altura { get; set; }

        [Required(ErrorMessage = "O campo largura é obrigatório")]
        public double Largura { get; set; }

        [Required(ErrorMessage = "O campo comprimento é obrigatório")]
        public double Comprimento { get; set; }

        [Required(ErrorMessage = "O campo valor é obrigatório")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "O campo de quantidade em estoque é obrigatório")]
        public int QuantidadeEmEstoque { get; set; }
        
        [Required(ErrorMessage = "O campo de centro de distribuiçao em estoque é obrigatório")]
        public int? CentroDeDistribuicaoId { get; set; }
        [Required(ErrorMessage = "O campo subCategoriaId é obrigatório")]
        public int? SubCategoriaId { get; set; }

    }
}
