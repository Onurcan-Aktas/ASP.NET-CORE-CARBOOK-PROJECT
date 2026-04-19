using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyCarBook.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Features_FeatureId",
                table: "CarFeatures");

            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "CarFeatures",
                newName: "FeatureID");

            migrationBuilder.RenameColumn(
                name: "CarFeatureId",
                table: "CarFeatures",
                newName: "CarFeatureID");

            migrationBuilder.RenameIndex(
                name: "IX_CarFeatures_FeatureId",
                table: "CarFeatures",
                newName: "IX_CarFeatures_FeatureID");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaytingValue = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "CarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CarID",
                table: "Reviews",
                column: "CarID");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Features_FeatureID",
                table: "CarFeatures",
                column: "FeatureID",
                principalTable: "Features",
                principalColumn: "FeatureID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Features_FeatureID",
                table: "CarFeatures");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.RenameColumn(
                name: "FeatureID",
                table: "CarFeatures",
                newName: "FeatureId");

            migrationBuilder.RenameColumn(
                name: "CarFeatureID",
                table: "CarFeatures",
                newName: "CarFeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_CarFeatures_FeatureID",
                table: "CarFeatures",
                newName: "IX_CarFeatures_FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Features_FeatureId",
                table: "CarFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "FeatureID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
