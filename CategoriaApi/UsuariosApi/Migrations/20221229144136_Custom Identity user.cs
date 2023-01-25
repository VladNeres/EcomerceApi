using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class CustomIdentityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "2d69742b-1824-40e0-90a9-239f4a04f878");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "63a98fcf-5e90-4329-9cf9-dc2b254e8a91");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d836469-859b-4ae2-be57-8e32705fd405", "AQAAAAEAACcQAAAAEKHTKXppTLO7oDZFZ+ZRGlAlJAF5vnMHOBmkU9UQ6EOXfV3SRgvHN92KD45cRm+hRw==", "1b58f1b2-ff79-4c59-9309-868d1690b807" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "d491b1b1-e912-433f-b0e6-a2faf1d05081");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "b6ef365d-9192-4d4d-988e-2fc0cbfa3542");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26694beb-bb3b-42d7-965a-2a0e1e3f3f73", "AQAAAAEAACcQAAAAEOoRIWfmiTcXPwUqhrQamm35EiIkWLLFfgSC7DBLL+P6HeJnWoPOwBK2Cz4Fx6LO0Q==", "375246fe-4c53-4146-8745-0b033a77af06" });
        }
    }
}
