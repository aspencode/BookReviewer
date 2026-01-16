using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReviewer.Migrations
{
    /// <inheritdoc />
    public partial class AddTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTag_Books_BooksId",
                table: "BookTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTag_Tags_TagsId",
                table: "BookTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookTag",
                table: "BookTag");

            migrationBuilder.RenameTable(
                name: "BookTag",
                newName: "BookTags");

            migrationBuilder.RenameIndex(
                name: "IX_BookTag_TagsId",
                table: "BookTags",
                newName: "IX_BookTags_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookTags",
                table: "BookTags",
                columns: new[] { "BooksId", "TagsId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTags_Books_BooksId",
                table: "BookTags",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTags_Tags_TagsId",
                table: "BookTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTags_Books_BooksId",
                table: "BookTags");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTags_Tags_TagsId",
                table: "BookTags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookTags",
                table: "BookTags");

            migrationBuilder.RenameTable(
                name: "BookTags",
                newName: "BookTag");

            migrationBuilder.RenameIndex(
                name: "IX_BookTags_TagsId",
                table: "BookTag",
                newName: "IX_BookTag_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookTag",
                table: "BookTag",
                columns: new[] { "BooksId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookTag_Books_BooksId",
                table: "BookTag",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTag_Tags_TagsId",
                table: "BookTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
