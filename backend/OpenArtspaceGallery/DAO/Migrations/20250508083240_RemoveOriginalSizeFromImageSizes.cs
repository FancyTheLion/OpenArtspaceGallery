using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOriginalSizeFromImageSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"ImagesSizes\" WHERE \"Name\" = 'Original';");
        }
    }
}
