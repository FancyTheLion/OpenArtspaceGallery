using Microsoft.EntityFrameworkCore.Migrations;
using OpenArtspaceGallery.DAO.Constants;
using OpenArtspaceGallery.DAO.Enums;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceIsPreviewWithStatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreview",
                table: "ImagesSizes");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ImagesSizes",
                type: "integer",
                nullable: false,
                defaultValue: ImagesSizesTypes.Normal);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
