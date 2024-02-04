using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TempRel.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Model1s",
                columns: table => new
                {
                    BaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model1s", x => x.BaseId);
                });

            migrationBuilder.CreateTable(
                name: "Model2s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    BaseId = table.Column<int>(type: "int", nullable: false),
                    Model1BaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model2s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Model2s_Model1s_Model1BaseId",
                        column: x => x.Model1BaseId,
                        principalTable: "Model1s",
                        principalColumn: "BaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Model2s_Model1BaseId",
                table: "Model2s",
                column: "Model1BaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Model2s");

            migrationBuilder.DropTable(
                name: "Model1s");
        }
    }
}
