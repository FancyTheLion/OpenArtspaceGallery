using Microsoft.EntityFrameworkCore.Migrations;
using OpenArtspaceGallery.DAO.Constants;
using OpenArtspaceGallery.DAO.Constants.MimeTypes;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class FillFileTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
            (
                "FileTypes",
                new[] { "Id", "MimeType", "Description" },
                new object[] { MimeTypes.Jpeg.Id, MimeTypes.Jpeg.MimeType, MimeTypes.Jpeg.Description }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.Png.Id, MimeTypes.Png.MimeType, MimeTypes.Png.Description }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.Gif.Id, MimeTypes.Gif.MimeType, MimeTypes.Gif.Description }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.Bmp.Id, MimeTypes.Bmp.MimeType, MimeTypes.Bmp.Description }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.WebP.Id, MimeTypes.WebP.MimeType, MimeTypes.WebP.Description }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.Tiff.Id, MimeTypes.Tiff.MimeType, MimeTypes.Tiff.Description }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.Ico.Id, MimeTypes.Ico.MimeType, MimeTypes.Ico.Description }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.Svg.Id, MimeTypes.Svg.MimeType, MimeTypes.Svg.Description }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.Heic.Id, MimeTypes.Heic.MimeType, MimeTypes.Heic.Description }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
