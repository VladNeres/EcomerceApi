using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class InserindocampoDataAtualizacaonamodelUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "dba70352-1807-440b-8b5b-607432bca148");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "6420a187-249a-4598-b529-ee6c57b99ecf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d85a1f44-db48-4a42-94ad-50788f74eef5", "AQAAAAEAACcQAAAAEM5TTd6y7/8ZmaZIH54FAO7TRan9NWt2yTvPri0fbRQIvu9s9vTn7YlaSatHVJpFow==", "f59b2428-0a92-48a2-af3c-27b4afe3ca06" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "039a8f63-bfc6-4e02-92a5-d11ff5b1cc81");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "e835d268-2a3b-4896-ae1b-3111f312bf2a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5541da5-ab42-492c-a134-4dbdb76b2f60", "AQAAAAEAACcQAAAAEJE49quqlxo98XavQ76ULSr2M9SyKtClWWUQ0F2/M0wrQ9CbN9uVtF2nIOrwJsWZ9w==", "dbf76d48-db2b-4baf-9051-7be3abf909c9" });
        }
    }
}
