using CategoriaApi.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Data.Dto.DtoSubCategoria
{
    public class ReadSubCategoriaDto
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public string DataCriacao { get; set; }
        public string DataAtualizacao { get; set; }
        [JsonIgnore]
        public Categoria Categoria { get; set; }
        public object Produtos { get; set; }

    }
}
