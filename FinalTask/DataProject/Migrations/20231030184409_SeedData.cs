using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //TODO order product scripts
            migrationBuilder.Sql
                (
                @"
                    USE [finaltaskdb] GO INSERT INTO [dbo].[Dealer] ([DealerId],[Name] ,[Address],[ProfitMargin],[Limit],[InsertDate],[UpdateDate],[IsActive]) VALUES(1,'Dealer1','Mr John Smith. 132, My Street, Bigtown BG23 4YZ.',2.5 ,1600 ,'2022-02-19 00:00:00.0000000', NULL ,1 GO




"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
