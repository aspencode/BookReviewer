using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReviewer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveParentBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Books_ParentBookId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ParentBookId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ParentBookId",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentBookId",
                table: "Books",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ParentBookId",
                table: "Books",
                column: "ParentBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Books_ParentBookId",
                table: "Books",
                column: "ParentBookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
