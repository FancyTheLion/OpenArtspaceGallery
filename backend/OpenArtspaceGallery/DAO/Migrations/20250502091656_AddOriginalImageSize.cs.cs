using Microsoft.EntityFrameworkCore.Migrations;
using OpenArtspaceGallery.DAO.Constants;
using OpenArtspaceGallery.DAO.Constants.ImagesSizes;

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
                new object[] { ImagesSizes.Original.Id, ImagesSizes.Original.Name, ImagesSizes.Original.Width, ImagesSizes.Original.Height, ImagesSizes.Original.Type }
            );
        }
    }
}
