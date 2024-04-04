using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollageMng.Migrations
{
    /// <inheritdoc />
    public partial class addtimetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
