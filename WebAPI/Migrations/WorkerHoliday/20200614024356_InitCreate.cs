using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations.WorkerHoliday
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkerHolidays",
                columns: table => new
                {
                    IdForH = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(type: "nvarchar(35)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    PMId = table.Column<int>(type: "int", nullable: false),
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
