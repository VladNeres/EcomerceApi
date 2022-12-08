using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.DtoProduto
{
    public class UpdateProdutoDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '/s]{1,1000}", ErrorMessage = "O campo nome não deve conter numeros ou caracteres especiais")]
        [StringLength(128, ErrorMessage = "Quantidade máxima de 128 caracteres excedido")]
        public string Nome { get; set; }
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '/s]{1,1000}", ErrorMessage = "O campo descrição não deve conter números ou caracteres especiais")]
        [Required(ErrorMessage = "O Campo descrição é obrigatório")]
        [StringLength(512, ErrorMessage = "Tamanho maximo de 512 caracteres excedido")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O Campo Peso é obrigatório")]
        public double Peso { get; set; }
        [Required(ErrorMessage = "O Campo Altura é obrigatório")]
        public double Altura { get; set; }
        [Required(ErrorMessage = "O Campo largura é obrigatório")]
        public double Largura { get; set; }
        [Required(ErrorMessage = "O Campo comprimento é obrigatório")]
        public double Comprimento { get; set; }
        [Required(ErrorMessage = "O Campo valor é obrigatório")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "O Campo de quantidade em estoque é obrigatório")]
        public int QuantidadeEmEstoque { get; set; }
        [Required(ErrorMessage = "O campo status é obrigatório")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "O campo centro de distribuição é obrigatório")]
        public int CentroDeDistribuicaoId { get; set; }
       
    }
}
