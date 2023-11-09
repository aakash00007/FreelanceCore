using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelanceDAL.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35403dae-8769-4773-b617-a5087d827284",
                column: "ConcurrencyStamp",
                value: "44c1a5e6-98f2-4cb4-9a55-b455fee56b92");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "649fca11-51e3-4db8-91d7-0bba63163280",
                column: "ConcurrencyStamp",
                value: "1c5c5965-59b3-4ebb-8bc4-c6176a94d41f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "422b7d11-6c54-436e-a04f-7619e7acf637",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60b28c36-a5d4-462f-8a61-5d1c94b73979", "AQAAAAEAACcQAAAAEO58sAdO1fV2ihFazRCL2jrV7W7CU5S42SYrWk0juOUnkQ7si+SKm021XfHG+oHuFg==", "689e3e53-d6d3-4ab4-99cc-7fb468b462b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5eedf264-7629-412a-937e-926ec371be3c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6cc2f486-2983-44f4-90eb-d1ff0b6f4ccd", "AQAAAAEAACcQAAAAEKfmBMJz1VV+Chyqx7ipXNRPZlfYi4jWwX+q+MXW8/P101/2KPOjZXUcpUQrIVlxTA==", "549a0cac-e51b-4cc3-97e2-0cbfaf9cd24c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
