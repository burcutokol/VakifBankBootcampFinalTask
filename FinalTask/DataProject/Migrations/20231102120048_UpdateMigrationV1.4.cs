using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrationV13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserLoginId",
                table: "User",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "UserLoginId",
                table: "Dealer",
                nullable: false,
                defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "UserLoginId",
               table: "User"
           );

            migrationBuilder.DropColumn(
                name: "UserLoginId",
                table: "Dealer"
            );
        }
    }
}
