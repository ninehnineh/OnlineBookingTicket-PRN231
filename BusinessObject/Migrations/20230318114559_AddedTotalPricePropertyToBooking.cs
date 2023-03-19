using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class AddedTotalPricePropertyToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "totalPrice",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "26ed6469-21e7-4b10-b669-6cad0f2dabcf");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "5f09e59f-6344-43c1-afac-6824b8d99a2f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63897c1e-3fa9-48e1-b694-6096c9b440da", "AQAAAAEAACcQAAAAEL3BeMsvk08Jyfo2Ncch/4QsYmnRxZcsF6OOxIeoFZJfx8ClI89pfmMV2MKu6Amkog==", "418dcb71-a817-44dd-8adf-2dd9da534661" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b8db1f8-688d-48c5-85ef-51b77f85ba65", "AQAAAAEAACcQAAAAEEYPYLYkqO57Ntv31B2BZ0Fm5hUWpN9p2EVSnnBuYUKEFYjbfaXsuXXiNOG284QWVw==", "2791228c-973e-4d20-9e85-70c5e5dced4d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "Bookings");

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
    }
}
