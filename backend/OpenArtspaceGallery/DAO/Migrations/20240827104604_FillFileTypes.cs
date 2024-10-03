using Microsoft.EntityFrameworkCore.Migrations;
using OpenArtspaceGallery.DAO.Constants;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class FillFileTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // TODO: Fix code style
            // TODO: Check for copypasta
            migrationBuilder.InsertData
            (
                "FileTypes",
                new[] { "Id", "MimeType", "Description" },
                new object[] { Types.TypeIdJpeg, Types.MimeTypeJpeg, Types.DescriptionTypeJpeg }
            );
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdPng, Types.MimeTypePng, Types.DescriptionTypePng });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdGif, Types.MimeTypeGif, Types.DescriptionTypeGif });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdBmp, Types.MimeTypeBmp, Types.DescriptionTypeBmp });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdWebP, Types.MimeTypeWebP, Types.DescriptionTypeWebP });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdTiff, Types.MimeTypeTiff, Types.DescriptionTypeTiff });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdIco, Types.MimeTypeIco, Types.DescriptionTypeIco });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdSvg, Types.MimeTypeSvg, Types.DescriptionTypeSvg });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdHeic, Types.MimeTypeHeic, Types.DescriptionTypeHeic });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
