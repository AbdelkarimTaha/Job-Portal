using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemExceptionLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemExceptionLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EX_LEVEL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EX_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ACTION_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EX_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EX_MESSAGE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STACK_TRACE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MESSAGE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SEVERITY = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemExceptionLogs", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemExceptionLogs");
        }
    }
}
