using CategoriaApi.Data.Dto.DtoSubCategoria;
using FluentResults;
using System.Collections.Generic;

namespace CategoriaApi.Interfaces
{
    public interface ISubCategoriaService
    {
        ReadSubCategoriaDto CriarSubCategoria(CreateSubCategoriaDto subCategoriaDto);
        Result DeletarSubCategoria(int id);
        Result EditarSubCategoria(int id, UpdateSubCategoriaDto subDto);
        List<ReadSubCategoriaDto> GetSubCategoria(string nome, bool? status, string ordem, int quantidadePorPagina);
    }
}
