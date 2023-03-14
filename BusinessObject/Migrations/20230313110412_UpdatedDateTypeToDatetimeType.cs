using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class UpdatedDateTypeToDatetimeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Starttime",
                table: "MovieShows",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Endtime",
                table: "MovieShows",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "MovieShows",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "fa2845e1-94bd-4104-a969-c0bbee76b4c6");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7027b852-58d9-4f63-8c20-07a0dacd4919");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a68a7373-1e9e-41f6-ba2f-70150fc90593", "AQAAAAEAACcQAAAAEDqws0GAnotx/fy89MpQXNqQn+2WHNDfwkNWuiF5OGiIQmpNKbgMjDPxO7fAXkixlg==", "7dab6cbe-44f7-4c4a-8533-5cc21392c888" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa760ae7-80f2-4ce4-8430-5b6ebf235f2f", "AQAAAAEAACcQAAAAEGzTEb/xaNEaELSmujQkxp180/sWtcbTF93qE6ewt3GiUh6k4T6zRPR9qU/xLTjt9w==", "583acf92-3eb7-4b72-83bd-2d49511bde61" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Starttime",
                table: "MovieShows",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Endtime",
                table: "MovieShows",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "MovieShows",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

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
    }
}
