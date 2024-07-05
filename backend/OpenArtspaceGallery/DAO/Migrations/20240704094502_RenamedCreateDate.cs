using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class RenamedCreateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Albums",
                newName: "CreationTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
