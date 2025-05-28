using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloggK.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTagRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTag_BlogPosts_BlogPostsId",
                table: "BlogPostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTag_Tags_TagsId",
                table: "BlogPostTag");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "BlogPostTag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "BlogPostsId",
                table: "BlogPostTag",
                newName: "BlogPostId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostTag_TagsId",
                table: "BlogPostTag",
                newName: "IX_BlogPostTag_TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTag_BlogPosts_BlogPostId",
                table: "BlogPostTag",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTag_Tags_TagId",
                table: "BlogPostTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTag_BlogPosts_BlogPostId",
                table: "BlogPostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTag_Tags_TagId",
                table: "BlogPostTag");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "BlogPostTag",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "BlogPostId",
                table: "BlogPostTag",
                newName: "BlogPostsId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostTag_TagId",
                table: "BlogPostTag",
                newName: "IX_BlogPostTag_TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTag_BlogPosts_BlogPostsId",
                table: "BlogPostTag",
                column: "BlogPostsId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTag_Tags_TagsId",
                table: "BlogPostTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
