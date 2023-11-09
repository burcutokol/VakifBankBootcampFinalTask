using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrationV15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "PaymentTypes",
               columns: table => new
               {
                   PaymentMethodId = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   PaymentMethodName = table.Column<string>(maxLength: 20, nullable: false),
                   Description = table.Column<string>(maxLength: 50, nullable: false),
                   InsertDate = table.Column<DateTime>(nullable: false),
                   UpdateDate = table.Column<DateTime>(nullable: true),
                   IsActive = table.Column<bool>(nullable: false, defaultValue: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_PaymentTypes", x => x.PaymentMethodId);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTypes");
        }
    }
}
