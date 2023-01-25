using CategoriaApi.Data.Dto.DtoCategoria;
using CategoriaApi.Model;
using System.Collections.Generic;

namespace CategoriaApi.Interfaces
{
    public interface ICategoriaRepository
    {
        public void AdicionarCategoria(Categoria catDto);
        public void AtualizarCategoria(Categoria catDto);
        public void ExcluirCategoria(Categoria categoria);

        public Categoria BuscarNomeCategoria(CreateCategoriaDto catDto);

        public Categoria BuscarCategoriaPorId(int id);

        public List<Categoria> BuscarListaCategorias();

        public IEnumerable<SubCategoria> BuscarSubId(int id);
        public void SalvarAlteraçoes();
    }
}
