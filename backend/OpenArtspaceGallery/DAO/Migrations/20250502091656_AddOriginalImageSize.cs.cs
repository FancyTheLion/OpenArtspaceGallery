using Microsoft.EntityFrameworkCore.Migrations;
using OpenArtspaceGallery.DAO.Constants;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class AddOriginalImageSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
            (
                "ImagesSizes",
                new[] { "Id", "Name", "Width", "Height", "Type" },
                new object[] { ImagesSizes.OriginalId, ImagesSizes.OriginalName, ImagesSizes.OriginalWidth, ImagesSizes.OriginalHeight, ImagesSizes.OriginalType }
            );
        }
    }
}
