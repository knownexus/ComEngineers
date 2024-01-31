using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComEngineers.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCode = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accelerometer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCode = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    XValue = table.Column<float>(type: "real", nullable: false),
                    YValue = table.Column<float>(type: "real", nullable: false),
                    ZValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accelerometer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accelerometer_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gyroscope",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCode = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    XAxis = table.Column<float>(type: "real", nullable: false),
                    YAxis = table.Column<float>(type: "real", nullable: false),
                    ZAxis = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyroscope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gyroscope_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeartRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCode = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeartRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeartRate_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Temperature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCode = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temperature_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accelerometer_SessionId",
                table: "Accelerometer",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Gyroscope_SessionId",
                table: "Gyroscope",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_HeartRate_SessionId",
                table: "HeartRate",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Temperature_SessionId",
                table: "Temperature",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accelerometer");

            migrationBuilder.DropTable(
                name: "Gyroscope");

            migrationBuilder.DropTable(
                name: "HeartRate");

            migrationBuilder.DropTable(
                name: "Temperature");

            migrationBuilder.DropTable(
                name: "Session");
        }
    }
}
