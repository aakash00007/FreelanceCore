using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelanceDAL.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_UserId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceTypeId",
                table: "Services",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35403dae-8769-4773-b617-a5087d827284",
                column: "ConcurrencyStamp",
                value: "cf49314d-f651-43b5-b80a-c791f82f209d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "649fca11-51e3-4db8-91d7-0bba63163280",
                column: "ConcurrencyStamp",
                value: "1da86acc-ad3b-4b2d-88c6-7e4cfb5a6e7c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "422b7d11-6c54-436e-a04f-7619e7acf637",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40cb1f8d-56cc-4a60-9a39-295b0e5e0701", "AQAAAAEAACcQAAAAEImYmEABVWv/U5Srmw70KnmiWQgYmx45vcp1//wSpHtHzp9leczjfEFq9y2Y/eZeGg==", "a467cdb0-d068-4b16-81ef-8f80ac5104b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5eedf264-7629-412a-937e-926ec371be3c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aae45709-8f33-41c2-9f9b-fcb0e745b5ce", "AQAAAAEAACcQAAAAEF/IGprpJ/B3NBLbh1feLZwJi3rdxsjiPoqvdroLtsP1QRlCXOaInTnOgdL/quG5Ww==", "e10de193-dbc5-403d-8dcb-5b42e69f94c3" });

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_UserId",
                table: "Services",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_UserId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceTypeId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35403dae-8769-4773-b617-a5087d827284",
                column: "ConcurrencyStamp",
                value: "6ee70e23-20c0-42c9-92b3-036d5f30aacb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "649fca11-51e3-4db8-91d7-0bba63163280",
                column: "ConcurrencyStamp",
                value: "c241ed17-0590-4483-b4e3-7d79c55cf736");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "422b7d11-6c54-436e-a04f-7619e7acf637",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fc1fe77-e99c-4cce-a5fd-aa4115a87064", "AQAAAAEAACcQAAAAECA7Q2riHgYZCZQ8NtnmS3B+Gu8kryG8gSuBDPDWqPs28KMq77IWWKGPJEVsa2ngJA==", "c6c18fbe-94b3-4975-b16c-6025b258d29f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5eedf264-7629-412a-937e-926ec371be3c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "657641f4-6fc3-4b2f-8b8b-176375024aa4", "AQAAAAEAACcQAAAAEJjVMn+qqCZIp22YZK+eakc04bR0c3bcfF47b/A8R1Qx6JlQnta0Rez7RZzGYOJDQw==", "be73b337-136d-4ac9-b29c-de3609db22cb" });

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_UserId",
                table: "Services",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "ServiceTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
