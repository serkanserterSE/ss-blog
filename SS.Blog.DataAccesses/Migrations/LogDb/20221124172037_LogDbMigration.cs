using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SS.Blog.DataAccesses.Migrations.LogDb
{
    public partial class LogDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "log");

            migrationBuilder.CreateTable(
                name: "ApiLog",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodType = table.Column<short>(type: "smallint", nullable: false),
                    MethodTypeName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    RequestHttpType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionLog",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiLog",
                schema: "log");

            migrationBuilder.DropTable(
                name: "ExceptionLog",
                schema: "log");
        }
    }
}
