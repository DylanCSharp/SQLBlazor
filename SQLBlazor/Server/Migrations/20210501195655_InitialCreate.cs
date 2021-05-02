using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLBlazor.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPLOYEES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMP_FIRSTNAME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EMP_SECONDNAME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EMP_EMAIL_ADDRESS = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEES", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPLOYEES");
        }
    }
}
