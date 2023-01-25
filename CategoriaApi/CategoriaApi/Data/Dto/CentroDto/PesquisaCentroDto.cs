namespace CategoriaApi.Data.Dto.CentroDto
{
    public class PesquisaCentroDto
    {
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public int? Numero { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Complemento { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public bool? Status { get; set; }
        public string Ordem { get; set; }
        public int ItensPorPagina { get; set; }
        public int Pagina { get; set; }


    }
}
