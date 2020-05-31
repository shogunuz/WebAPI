using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations.WorkerHoliday
{
    public partial class InitialCreateHoliday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkerHolidays",
                columns: table => new
                {
                    IdForH = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PMId = table.Column<int>(type: "int", nullable: false),
                    FIO = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DateStart = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    DateEnd = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerHolidays", x => x.IdForH);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkerHolidays");
        }
    }
}
