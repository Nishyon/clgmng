using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollageMng.Migrations
{
    /// <inheritdoc />
    public partial class addcourcetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImbaCource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sem = table.Column<int>(type: "int", nullable: false),
                    code1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code7 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImbaCource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImcaCource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sem = table.Column<int>(type: "int", nullable: false),
                    code1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code5 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImcaCource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MbaCource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sem = table.Column<int>(type: "int", nullable: false),
                    code1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code7 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MbaCource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "McaCources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sem = table.Column<int>(type: "int", nullable: false),
                    code1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code5 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_McaCources", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImbaCource");

            migrationBuilder.DropTable(
                name: "ImcaCource");

            migrationBuilder.DropTable(
                name: "MbaCource");

            migrationBuilder.DropTable(
                name: "McaCources");
        }
    }
}
