using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollageMng.Migrations
{
    /// <inheritdoc />
    public partial class addTt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sem = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lec1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lec2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lec3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lec4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lec5 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tt", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tt");
        }
    }
}
