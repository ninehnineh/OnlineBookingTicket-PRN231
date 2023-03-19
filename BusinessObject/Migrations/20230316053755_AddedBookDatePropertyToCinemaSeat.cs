using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class AddedBookDatePropertyToCinemaSeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookDate",
                table: "CinemaSeats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookDate",
                table: "CinemaSeats");

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
    }
}
