using Microsoft.EntityFrameworkCore.Migrations;

namespace CategoriaApi.Migrations
{
    public partial class Criandocamposdedistribuiçãoeiddecategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CentroDeDistribuicao",
                table: "Produtos",
                type: "text",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CentroDeDistribuicao",
                table: "Produtos");
        }
    }
}
