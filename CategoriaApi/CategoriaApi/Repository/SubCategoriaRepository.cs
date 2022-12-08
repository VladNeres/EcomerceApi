using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Repository
{
    public class SubCategoriaRepository
    {
        DatabaseContext _context;

        public SubCategoriaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void AddSubCategoria(SubCategoria subCat)
        {
            _context.Add(subCat);
            _context.SaveChanges();
        }

        public void DeletarSubCat(SubCategoria sub)
        {
            _context.Remove(sub);
            _context.SaveChanges();
        }

        public SubCategoria RecuperarSubPorId(int id)
        {
            var sub = _context.SubCategorias.FirstOrDefault(sub => sub.Id == id);
            return sub;
        }

        public SubCategoria VerificaSeExistePeloNome(CreateSubCategoriaDto subDto )
        {
            SubCategoria subCatNome= _context.SubCategorias.FirstOrDefault(sub=> sub.Nome.ToUpper() == subDto.Nome.ToUpper());
            return subCatNome;
        }

        public Categoria RecuperaCategoriaPorId(CreateSubCategoriaDto subDto)
        {
            Categoria cat = _context.Categorias.FirstOrDefault(cat => cat.Id == subDto.CategoriaId);
            return cat;
        }

       public void SalvarAlteracoes()
        {
            _context.SaveChanges();
        }

        public List<SubCategoria> RecuperarListaDeSub()
        {
            var sub = _context.SubCategorias.ToList();
            return sub;
        }
    }
}
