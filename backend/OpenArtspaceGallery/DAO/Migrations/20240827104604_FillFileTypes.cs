﻿using Microsoft.EntityFrameworkCore.Migrations;
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
                new object[] { Types.TypeIdJpeg, Types.TypeMimeJpeg, Types.TypeDescriptionJpeg }
            );
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdPng, Types.TypeMimePng, Types.TypeDescriptionPng });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdGif, Types.TypeMimeGif, Types.TypeDescriptionGif });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdBmp, Types.TypeMimeBmp, Types.TypeDescriptionBmp });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdWebP, Types.TypeMimeWebP, Types.TypeDescriptionWebP });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdTiff, Types.TypeMimeTiff, Types.TypeDescriptionTiff });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdIco, Types.TypeMimeIco, Types.TypeDescriptionIco });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdSvg, Types.TypeMimeSvg, Types.TypeDescriptionSvg });
            
            migrationBuilder.InsertData(
                table: "FileTypes",
                columns: new[] { "Id", "MimeType", "Description" },
                values: new object[]
                    { Types.TypeIdHeic, Types.TypeMimeHeic, Types.TypeDescriptionHeic });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
