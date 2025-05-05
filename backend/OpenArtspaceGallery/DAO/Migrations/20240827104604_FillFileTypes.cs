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
            migrationBuilder.InsertData
            (
                "FileTypes",
                new[] { "Id", "MimeType", "Description" },
                new object[] { MimeTypes.TypeIdJpeg, MimeTypes.TypeMimeJpeg, MimeTypes.TypeDescriptionJpeg }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.TypeIdPng, MimeTypes.TypeMimePng, MimeTypes.TypeDescriptionPng }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.TypeIdGif, MimeTypes.TypeMimeGif, MimeTypes.TypeDescriptionGif }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.TypeIdBmp, MimeTypes.TypeMimeBmp, MimeTypes.TypeDescriptionBmp }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.TypeIdWebP, MimeTypes.TypeMimeWebP, MimeTypes.TypeDescriptionWebP }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.TypeIdTiff, MimeTypes.TypeMimeTiff, MimeTypes.TypeDescriptionTiff }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.TypeIdIco, MimeTypes.TypeMimeIco, MimeTypes.TypeDescriptionIco }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.TypeIdSvg, MimeTypes.TypeMimeSvg, MimeTypes.TypeDescriptionSvg }
            );
            
            migrationBuilder.InsertData
            (
                 "FileTypes",
                 new[] { "Id", "MimeType", "Description" },
                 new object[] { MimeTypes.TypeIdHeic, MimeTypes.TypeMimeHeic, MimeTypes.TypeDescriptionHeic }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
