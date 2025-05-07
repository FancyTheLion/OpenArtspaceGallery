using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.Internal;
using OpenArtspaceGallery.DAO.Constants;
using OpenArtspaceGallery.DAO.Constants.ImagesSizes;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailFlagToImageSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"update \"ImagesSizes\" set \"IsPreview\" = true where \"Id\" = '{ ImagesSizes.Thumbnail.Id}'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
