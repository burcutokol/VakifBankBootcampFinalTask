using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrationV16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "Message",
              schema: "dbo",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                  UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                  IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                  MessageContent = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                  SenderId = table.Column<int>(type: "int", nullable: false),
                  RecevierId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Message", x => x.Id);
              });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Message",
              schema: "dbo");
        }
    }
}
