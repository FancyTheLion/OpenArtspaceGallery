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
                new object[] { Sizes.ThumbnailId, Sizes.ThumbnailName, Sizes.ThumbnailWidth, Sizes.ThumbnailHeight }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
