using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Model;
using FluentResults;
using System.Collections.Generic;

namespace CategoriaApi.Interfaces
{
    public interface ISubCategoriaRepository
    {
        void AddSubCategoria(SubCategoria subCat);
        void DeletarSubCat(SubCategoria sub);
        Categoria RecuperaCategoriaPorId(CreateSubCategoriaDto subDto);
        List<SubCategoria> RecuperarListaDeSub();
        SubCategoria RecuperarSubPorId(int id);
        void SalvarAlteracoes();
        SubCategoria VerificaSeExistePeloNome(CreateSubCategoriaDto subDto);
    }
}
