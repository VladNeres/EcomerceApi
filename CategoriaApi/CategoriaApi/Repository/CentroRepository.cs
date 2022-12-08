using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Model;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CategoriaApi.Repository
{
    public class CentroRepository
    {
        private DatabaseContext _context;
        private readonly IDbConnection _dbConnection;

        public CentroRepository(DatabaseContext centroRepository, IDbConnection connection)
        {
            _dbConnection = connection;
            _context = centroRepository;
        }

        public void AddCentro(CentroDeDistribuicao centro, CentroDeDistribuicao endereco)
        {
            centro.UF = endereco.UF;
            centro.Localidade= endereco.Localidade;
            centro.CEP = endereco.CEP;
            centro.Logradouro= endereco.Logradouro;
            centro.Bairro = endereco.Bairro;

            _context.Add(centro);
            _context.SaveChanges();
        }

        public void ExcluirCentro(CentroDeDistribuicao centro)
        {
            _context.Remove(centro);
            _context.SaveChanges();
        }

        public CentroDeDistribuicao RecuperarCentroPorId(int id)
        {
            CentroDeDistribuicao centro = _context.Centros.FirstOrDefault(centro => centro.Id == id);
                return centro;
        }

        public CentroDeDistribuicao RetornarNomeDocentro(CreateCentroDto centroDto)
        {
            CentroDeDistribuicao centroNome = _context.Centros.FirstOrDefault(centro => centro.Nome.ToLower() == centroDto.Nome.ToLower());
            return centroNome;
        }

        public CentroDeDistribuicao RetornarEndereco(CreateCentroDto centroDto)
        {
            if (centroDto.CEP.Length == 8)
            {
               centroDto.CEP = centroDto.CEP.Insert(5, "-");
               System.Console.WriteLine(centroDto.CEP);
            }
           var endereco = _context.Centros.FirstOrDefault(centro=> centro.CEP == centroDto.CEP && centroDto.Numero == centro.Numero);
            return endereco;
        }
        public void Salvar()
        {
            _context.SaveChanges();
        }

        public List<CentroDeDistribuicao> GetCentroDeDistribuicao(CentroPesquisa pesquisa)
        {
            var sql = "SELECT * FROM Centros WHERE ";

            if (pesquisa.Nome != null)
            {
                sql += "Nome LIKE \"%" + pesquisa.Nome + "%\" and ";
            }
            if (pesquisa.Logradouro != null)
            {
                sql += "Logradouro LIKE \"%" + pesquisa.Logradouro + "%\" and ";
            }
            if (pesquisa.Status != null)
            {
                sql += "Status = @Status and ";
            }
            if (pesquisa.CEP != null)
            {
                sql += "Cep = @CEP and ";
            }
            if (pesquisa.Bairro != null)
            {
                sql += "Bairro LIKE \"%" + pesquisa.Bairro + "%\" and ";
            }
            if (pesquisa.Localidade != null)
            {
                sql += "Localidade LIKE \"%" + pesquisa.Localidade + "%\" and ";
            }
            if (pesquisa.Complemento != null)
            {
                sql += "Complemento LIKE \"%" + pesquisa.Complemento + "%\" and ";
            }
            if (pesquisa.UF != null)
            {
                sql += "Uf = @UF and ";
            }
            if (pesquisa.Numero != null)
            {
                sql += "Numero = @Numero and ";
            }

            if (pesquisa.Nome == null && pesquisa.Logradouro == null && pesquisa.CEP == null && pesquisa.Bairro == null 
                && pesquisa.Localidade == null && pesquisa.Complemento == null &&
                pesquisa.UF == null && pesquisa.Numero == null && pesquisa.Status == null)
            {
                sql = sql.Replace("WHERE", " ");
            }
            else
            {
                sql = sql.Replace("and", " ");
            }
            if (pesquisa.Ordem != null)
            {
                if (pesquisa.Ordem != "Decrescente")
                {
                    sql += " ORDER BY Nome";
                }
                else
                {
                    sql += " ORDER BY Nome DESC";
                }
            }

            var result = _dbConnection.Query<CentroDeDistribuicao>(sql, new
            {
                      Nome = pesquisa.Nome,
                    Status = pesquisa.Status,
                Logradouro = pesquisa.Logradouro,
                       Cep = pesquisa.CEP,
                    Bairro = pesquisa.Bairro,
                Localidade = pesquisa.Localidade,
               Complemento = pesquisa.Complemento,
                        Uf = pesquisa.UF,
                    Numero = pesquisa.Numero
            });

            if (pesquisa.Pagina > 0 && pesquisa.ItensPorPagina > 0 && pesquisa.ItensPorPagina <= 10)
            {
                var resultado = result.Skip((pesquisa.Pagina - 1) * pesquisa.ItensPorPagina).Take(pesquisa.ItensPorPagina).ToList();
                return resultado;

            }

            var resultadoSemPaginacao = result.Skip(0).Take(25).ToList();
            return resultadoSemPaginacao;

        }
    }
}

