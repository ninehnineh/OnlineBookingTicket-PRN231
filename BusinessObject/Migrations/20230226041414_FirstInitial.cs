using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class FirstInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Duration = table.Column<DateTime>(type: "date", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalCinemaHalls = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cinemas_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CinemaHalls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Totalseats = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CinemaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaHalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CinemaHalls_Cinemas_CinemaID",
                        column: x => x.CinemaID,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CinemaSeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CinemaHallID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CinemaSeats_CinemaHalls_CinemaHallID",
                        column: x => x.CinemaHallID,
                        principalTable: "CinemaHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Starttime = table.Column<DateTime>(type: "date", nullable: false),
                    Endtime = table.Column<DateTime>(type: "date", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    CinemaHallID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieShows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieShows_CinemaHalls_CinemaHallID",
                        column: x => x.CinemaHallID,
                        principalTable: "CinemaHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieShows_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Timestamp = table.Column<DateTime>(type: "date", nullable: false),
                    MovieShowID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_MovieShows_MovieShowID",
                        column: x => x.MovieShowID,
                        principalTable: "MovieShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShowSeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    MovieShowID = table.Column<int>(type: "int", nullable: false),
                    CinemaSeatID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShowSeats_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShowSeats_CinemaSeats_CinemaSeatID",
                        column: x => x.CinemaSeatID,
                        principalTable: "CinemaSeats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShowSeats_MovieShows_MovieShowID",
                        column: x => x.MovieShowID,
                        principalTable: "MovieShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MovieShowID",
                table: "Bookings",
                column: "MovieShowID");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaHalls_CinemaID",
                table: "CinemaHalls",
                column: "CinemaID");

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_CityID",
                table: "Cinemas",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaSeats_CinemaHallID",
                table: "CinemaSeats",
                column: "CinemaHallID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieShows_CinemaHallID",
                table: "MovieShows",
                column: "CinemaHallID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieShows_MovieID",
                table: "MovieShows",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowSeats_BookingID",
                table: "ShowSeats",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowSeats_CinemaSeatID",
                table: "ShowSeats",
                column: "CinemaSeatID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowSeats_MovieShowID",
                table: "ShowSeats",
                column: "MovieShowID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowSeats");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "CinemaSeats");

            migrationBuilder.DropTable(
                name: "MovieShows");

            migrationBuilder.DropTable(
                name: "CinemaHalls");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Cinemas");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
