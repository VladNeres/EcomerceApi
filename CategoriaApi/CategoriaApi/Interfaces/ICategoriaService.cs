using CategoriaApi.Data.Dto.DtoCategoria;
using FluentResults;
using System.Collections.Generic;

namespace CategoriaApi.Interfaces
{
    public interface ICategoriaService
    {
        
        ReadCategoriaDto AdicionarCategoria(CreateCategoriaDto categoriaDto);
        Result DeletarCategoria(int id);
        Result EditarCategoria(int id, UpdateCategoriaDto categoriaDto);
        List<ReadCategoriaDto> GetCategoria(string nome, bool? status, int quantidadePorPagina, string ordem);
    }
}
