using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationApp.DataAccessLayer.Migrations
{
    public partial class qwer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PicturePrintingEditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Picture = table.Column<byte[]>(nullable: true),
                    PrintingEditionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicturePrintingEditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PicturePrintingEditions_PrintingEditions_PrintingEditionId",
                        column: x => x.PrintingEditionId,
                        principalTable: "PrintingEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PicturePrintingEditions_PrintingEditionId",
                table: "PicturePrintingEditions",
                column: "PrintingEditionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PicturePrintingEditions");
        }
    }
}
