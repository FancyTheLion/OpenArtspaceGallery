using Microsoft.EntityFrameworkCore.Migrations;
using OpenArtspaceGallery.DAO.Constants;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailImageSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
            (
                "ImagesSizes",
                new[] { "Id", "Name", "Width", "Height" },
                new object[] { ImagesSizes.ThumbnailId, ImagesSizes.ThumbnailName, ImagesSizes.ThumbnailWidth, ImagesSizes.ThumbnailHeight }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
