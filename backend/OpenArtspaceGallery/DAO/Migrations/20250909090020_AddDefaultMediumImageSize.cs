using Microsoft.EntityFrameworkCore.Migrations;
using OpenArtspaceGallery.DAO.Constants.ImagesSizes;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultMediumImageSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
            (
                "ImagesSizes",
                new[] { "Id", "Name", "Width", "Height", "Type" },
                new object[] { ImagesSizes.MediumDefault.Id, ImagesSizes.MediumDefault.Name, ImagesSizes.MediumDefault.Width, ImagesSizes.MediumDefault.Height, ImagesSizes.MediumDefault.Type }
            );
        }
    }
}
