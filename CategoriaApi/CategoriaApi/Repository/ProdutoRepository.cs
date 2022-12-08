using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoProduto;
using CategoriaApi.Model;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CategoriaApi.Repository
{
    public class ProdutoRepository
    {

        private DatabaseContext _context;
        private readonly IDbConnection _dbConnection;

        public ProdutoRepository(DatabaseContext context, IDbConnection conection)
        {
            _context = context;
            _dbConnection = conection;
        }

        public void AddProduto(Produto produto)
        {
            _context.Add(produto);
            _context.SaveChanges();
        }

        //public async Task<bool> UpdateAsync(Produto produto)
        //{
        //    var result = await _dbConnection.UpdateAsync(produto);
        //    return result;
        //}
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbConnection.DeleteAsync(new Produto { Id = id });
            _context.SaveChanges();
            return result;

        }
        //public async Task<Produto> GetByIdAsync(int id)
        //{
        //    var result = await _dbConnection.GetAsync<Produto>(id);
        //    return result;
        //}

        //public async Task<IReadOnlyList<Produto>> GetAllAsync()
        //{

        //    var result = await _dbConnection.GetAllAsync<Produto>();
        //    return result.ToList();
        //}

        public SubCategoria SubCategoriaID(CreateProdutoDto produtoDto)
        {
            SubCategoria sub = _context.SubCategorias.FirstOrDefault(sub => sub.Id == produtoDto.SubCategoriaId);
            return sub;
        }

        public CentroDeDistribuicao CentroDeDistribuicaoID(CreateProdutoDto prodDto)
        {
            CentroDeDistribuicao centro = _context.Centros.FirstOrDefault(centro => centro.Id == prodDto.CentroDeDistribuicaoId);
            return centro;
        }

        public Produto VerificaSeJaExiste(CreateProdutoDto produtoDto)
        {
           Produto produto= _context.Produtos.FirstOrDefault(produtoNome => produtoNome.Nome.ToUpper() == produtoDto.Nome.ToUpper());
            return produto;
        }

        public Produto RecuperarProdutoPorId(int id)
        {
            Produto produto = _context.Produtos.FirstOrDefault(prod => prod.Id == id);
            return produto;
        }
        public void SalvarAlteracoes()
        {
            _context.SaveChanges();
        }
        public List<Produto> PesquisaComFiltros(string nome, bool? status, double? peso, double? altura, double? largura,
            double? comprimento, double? valor, int? estoque, string ordem, int itensPorPagina, int pagina)
        {
            var sql = "SELECT * FROM Produtos WHERE ";

            if (nome != null)
            {
                sql += "Nome LIKE \"%" + nome + "%\" and ";
            }
            if (status != null)
            {
                sql += "Status = @status and ";
            }
            if (peso != null)
            {
                sql += "Peso = @peso and ";
            }
            if (altura != null)
            {
                sql += "Altura = @altura and ";
            }
            if (largura != null)
            {
                sql += "Largura = @largura and ";
            }
            if (comprimento != null)
            {
                sql += "Comprimento = @comprimento and ";
            }
            if (valor != null)
            {
                sql += "Valor = @valor and ";
            }
            if (estoque != null)
            {
                sql += "Estoque = @estoque and ";
            }

            if (nome == null && estoque == null && valor == null && comprimento == null && largura == null &&
                altura == null && peso == null && status == null)
            {
                var PosicaoDoWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(PosicaoDoWhere);
            }
            else
            {
                var posicaoDoAnd = sql.LastIndexOf("and");
                sql = sql.Remove(posicaoDoAnd);
            }
            if (ordem != null)
            {
                if (ordem.ToUpper() != "DECRESCENTE")
                {
                    sql += " ORDER BY Nome";
                }
                else
                {
                    sql += " ORDER BY Nome DESC";
                }
            }
            if (itensPorPagina > 0)
            {
                sql.Take(itensPorPagina);
            }

            var result = _dbConnection.Query<Produto>(sql, new
            {
                Nome = nome,
                Status = status,
                Peso = peso,
                Altura = altura,
                Largura = largura,
                Comprimento = comprimento,
                Valor = valor,
                Estoque = estoque
            });

            var resultadoSemPaginacao = result.Skip(0).Take(25).ToList();
            return resultadoSemPaginacao;

        }
    }
}

