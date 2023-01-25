using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoCategoria;
using CategoriaApi.Interfaces;
using CategoriaApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Repository
{
    public class CategoriaRepository:ICategoriaRepository
    {
        private readonly DatabaseContext _context;
        public CategoriaRepository(DatabaseContext context)
        {
            _context = context;
        }


        public void AdicionarCategoria(Categoria catDto)
        {
            _context.Add(catDto);
            _context.SaveChanges();
        }

        public void AtualizarCategoria(Categoria catDto)
        {
            _context.Update(catDto);
            _context.SaveChanges();
        }

        public void ExcluirCategoria( Categoria categoria )
        {
            _context.Remove(categoria);
            _context.SaveChanges();
        }

        public Categoria BuscarNomeCategoria(CreateCategoriaDto catDto)
        {
            Categoria categoriaNome = _context.Categorias.FirstOrDefault(categoriaNome => categoriaNome.Nome.ToUpper() == catDto.Nome.ToUpper());
            return categoriaNome;
        }

        public Categoria BuscarCategoriaPorId(int id)
        {
            Categoria categoriaId = _context.Categorias.FirstOrDefault(cat => cat.Id == id);
            return categoriaId;
        }

        public List<Categoria> BuscarListaCategorias()
        {
            List<Categoria> categorias = _context.Categorias.ToList();
            return categorias;
        }
        public IEnumerable <SubCategoria> BuscarSubId(int id)
        {
            IEnumerable<SubCategoria> subCategoriaId = _context.SubCategorias.Where(sub=> sub.CategoriaId == id);
            return subCategoriaId;
        }

        public void SalvarAlteraçoes()
        {
            _context.SaveChanges();
        }
    }
}
