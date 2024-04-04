using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollageMng.Migrations
{
    /// <inheritdoc />
    public partial class addtimetable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Login",
                table: "Login");

            migrationBuilder.RenameTable(
                name: "Login",
                newName: "Login_Data");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Login_Data",
                table: "Login_Data",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Timetable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lec1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lec2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lec3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lec4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lec5 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetable", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timetable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Login_Data",
                table: "Login_Data");

            migrationBuilder.RenameTable(
                name: "Login_Data",
                newName: "Login");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Login",
                table: "Login",
                column: "Id");
        }
    }
}
