using Microsoft.EntityFrameworkCore.Migrations;
using OpenArtspaceGallery.DAO.Constants;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class AddNewThumbnailImageSizecs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
            (
                "ImagesSizes",
                new[] { "Id", "Name", "Width", "Height", "Type" },
                new object[] { Sizes.ThumbnailId, Sizes.ThumbnailName, Sizes.ThumbnailWidth, Sizes.ThumbnailHeight, Sizes.ThumbnailType }
            );
        }
    }
}
