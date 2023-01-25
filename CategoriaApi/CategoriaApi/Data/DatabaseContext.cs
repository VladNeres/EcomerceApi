using CategoriaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CategoriaApi.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SubCategoria>().
                HasOne(subCategoria => subCategoria.Categoria).
                WithMany(categoria => categoria.SubCategoria).
                HasForeignKey(subCategoria => subCategoria.CategoriaId);

            builder.Entity<Produto>()
                .HasOne(produto => produto.Subcategoria)
                .WithMany(subCategoria => subCategoria.Produtos)
                .HasForeignKey(produto => produto.SubCategoriaId);

            builder.Entity<Produto>()
                .HasOne(produto=> produto.CentrodeDistribuicao)
                .WithMany(Centro=> Centro.Produtos)
                .HasForeignKey(Produto=>Produto.CentroDeDistribuicaoId);      
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<SubCategoria> SubCategorias { get; set; }
        public DbSet<Produto> Produtos { get; set; } 
        public DbSet<CentroDeDistribuicao> Centros { get; set; }
    }
}
