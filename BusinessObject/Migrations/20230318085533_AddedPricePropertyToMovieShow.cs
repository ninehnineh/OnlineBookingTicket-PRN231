using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class AddedPricePropertyToMovieShow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "MovieShows",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8b509840-765d-461d-8915-54529930baf1");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "1ac3c5bb-69fb-44f7-8aba-8d9a4e811b3e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9ed2370-8a49-4fa8-9e50-c1f7f1c60cf4", "AQAAAAEAACcQAAAAEAFeYlSUR9ONORfwVOO3sq/YG/1U6eT1Lhmu7tIZmWEUy9HQPrkRP/wQCGE475lR1A==", "e50b528b-8b74-4f1e-82c5-5a9f336a5ad4" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a76b31b0-525e-48cc-9dd8-7ea4469b4d7f", "AQAAAAEAACcQAAAAENha3gGSpSAHvVhyaISR/JkbzMHVGXO4FkVn1KBm5/9cZDteUf4on6baErrUalUcFA==", "279ca109-6eb6-476c-8bd1-f6eca3ca70a1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "MovieShows");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "4a4c5c36-d536-4f88-9b39-a620ecd41fee");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "5f3ac53b-90bc-4065-9048-c23295b021ec");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7eaaba37-78ea-4016-b938-0429f76cf9a9", "AQAAAAEAACcQAAAAEARzQQuzyutPh3g1TKAezEFBYYVbaYiy7B9EVabWxA6dH2xOFIacbm/8Ne5u/4/OTQ==", "ac12d3b8-4e6b-45f8-858b-8bbee15ef85b" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3207d67-caaa-4c4d-9dd3-b96073ae1001", "AQAAAAEAACcQAAAAECHAwZyfyUxVWQ9s16dVNMtaaZ1YiqXoiLMZ4ZcDIABYNnXb4+qOY/DM/YnjqGG20Q==", "d04e22b9-1a19-4a05-ada8-5acd4fd8b1a3" });
        }
    }
}
