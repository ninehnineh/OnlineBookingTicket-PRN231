using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class SeedingRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "8d618ed7-018b-4860-8a4a-bfa8511d24b6", "Customer", "CUSTOMER" },
                    { "2", "6ff10f6a-81c5-455b-afd9-282b5282bd49", "Manager", "MANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9310409b-5a65-4824-9e9b-d5d3605f592f", "AQAAAAEAACcQAAAAELKq7ScAQXfZKxbSDKX299NsHXd06BZ4bFLLiNXvqqD5N8jkNn+qsVuNrXeFLP8CTg==", "635aef74-0ae2-462a-ad3c-b2aafd68b34c" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8432c54a-476b-4b91-b64d-bcc82dc86d89", "AQAAAAEAACcQAAAAEHEFpqgy1+alD1gwHl9betDGBkBUJ980//coHxWT/k6tgrM1CEvAs35FaWs3R6ffLA==", "cb003880-3168-4c79-83f2-ae86df7fd207" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRoles",
                table: "AppRoles");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.RenameTable(
                name: "AppRoles",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94e6773a-b686-4eb0-98a7-314ed1c6a961", "AQAAAAEAACcQAAAAEEkOLlQUJaKfU0f1X59neh5eU7/OOh/neEeBVJ3HS0J3PfJb6ZYYJ2b136BFNdH1TA==", "d24ae239-1774-4a83-a850-beb0e95a71b6" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f2b1fd8-43a5-49d5-b733-215089070522", "AQAAAAEAACcQAAAAELQEYDvvTEJyyimozTGwlX2aFUZqhgALiG8FMPmTgu6zdGrxWHfnSiIlBjq83B2ihQ==", "719e7e53-be4c-454d-b642-89b36d3a0bdf" });
        }
    }
}
