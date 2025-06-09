using Microsoft.EntityFrameworkCore.Migrations;
using OpenArtspaceGallery.DAO.Constants.ImagesSizes;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class RestoreThumbnailImageSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
            (
                "ImagesSizes",
                new[] { "Id", "Name", "Width", "Height", "Type" },
                new object[] { ImagesSizes.Thumbnail.Id, ImagesSizes.Thumbnail.Name, ImagesSizes.Thumbnail.Width, ImagesSizes.Thumbnail.Height, ImagesSizes.Thumbnail.Type }
            );
        }

    }
}
