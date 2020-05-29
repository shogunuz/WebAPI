using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations.WorkerHoliday
{
    public partial class changetablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkerHolidays",
                columns: table => new
                {
                    PMId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerHolidays", x => x.PMId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkerHolidays");
        }
    }
}
