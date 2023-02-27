using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class SeedingUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "bee5166e-ade6-4d22-b616-6e8bc0b52cce");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "21167ed8-fbc4-4674-bc71-1d88d0d1a94b");

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" }
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad286ba5-1f2d-438d-967b-d5af54dd11a6", "AQAAAAEAACcQAAAAEF6oekZwmZVX+Ci8YZujUsCXFc7splKKUZt9PLKSCPtmM+FE72uoCm2mkvgbVCEpTg==", "05816665-e597-4a60-8c13-1a545216393c" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "171d3993-c1eb-4924-b54a-95b0251c94da", "AQAAAAEAACcQAAAAEFpHNS9A8McaSYDJhN13l044L0EFp6tnkfca2lV9CzL2gd/rBeXuWNU76zDw2ulFFw==", "3631945d-086a-4265-8e3d-1396e2f86943" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserRoles",
                table: "AppUserRoles");

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.RenameTable(
                name: "AppUserRoles",
                newName: "UserRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8d618ed7-018b-4860-8a4a-bfa8511d24b6");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6ff10f6a-81c5-455b-afd9-282b5282bd49");

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
    }
}
