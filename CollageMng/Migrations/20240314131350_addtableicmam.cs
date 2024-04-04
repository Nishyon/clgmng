using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollageMng.Migrations
{
    /// <inheritdoc />
    public partial class addtableicmam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Icmarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sem = table.Column<int>(type: "int", nullable: false),
                    Stuno = table.Column<int>(type: "int", nullable: false),
                    S1 = table.Column<int>(type: "int", nullable: false),
                    S2 = table.Column<int>(type: "int", nullable: false),
                    S3 = table.Column<int>(type: "int", nullable: false),
                    S4 = table.Column<int>(type: "int", nullable: false),
                    S5 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icmarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mammarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sem = table.Column<int>(type: "int", nullable: false),
                    Stuno = table.Column<int>(type: "int", nullable: false),
                    S1 = table.Column<int>(type: "int", nullable: false),
                    S2 = table.Column<int>(type: "int", nullable: false),
                    S3 = table.Column<int>(type: "int", nullable: false),
                    S4 = table.Column<int>(type: "int", nullable: false),
                    S5 = table.Column<int>(type: "int", nullable: false),
                    S6 = table.Column<int>(type: "int", nullable: false),
                    S7 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mammarks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Icmarks");

            migrationBuilder.DropTable(
                name: "Mammarks");
        }
    }
}
