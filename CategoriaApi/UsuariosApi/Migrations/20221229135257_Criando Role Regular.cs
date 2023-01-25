using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class CriandoRoleRegular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "b6ef365d-9192-4d4d-988e-2fc0cbfa3542");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99997, "d491b1b1-e912-433f-b0e6-a2faf1d05081", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26694beb-bb3b-42d7-965a-2a0e1e3f3f73", "AQAAAAEAACcQAAAAEOoRIWfmiTcXPwUqhrQamm35EiIkWLLFfgSC7DBLL+P6HeJnWoPOwBK2Cz4Fx6LO0Q==", "375246fe-4c53-4146-8745-0b033a77af06" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "4ddb2d8d-24de-4efd-a55e-cd5e76f9dd7a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da394679-ab23-459b-9425-91d5a6d725a5", "AQAAAAEAACcQAAAAEBEaZvfrSeWrOTFrFPz+B7CWIe6Bgrs07wXKheWxIzdsnzkN8SG0O8mhIWnDp4kbyQ==", "d905d702-23a6-4b36-8fcc-bdbee04379de" });
        }
    }
}
