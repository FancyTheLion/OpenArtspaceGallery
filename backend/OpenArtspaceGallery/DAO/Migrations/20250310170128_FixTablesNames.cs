using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    /// <inheritdoc />
    public partial class FixTablesNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileTypes_TypeId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageFileDbo_Files_FileId",
                table: "ImageFileDbo");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageFileDbo_ImagesSizes_SizeId",
                table: "ImageFileDbo");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageFileDbo_Images_ImageId",
                table: "ImageFileDbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageFileDbo",
                table: "ImageFileDbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileTypes",
                table: "FileTypes");

            migrationBuilder.RenameTable(
                name: "ImageFileDbo",
                newName: "ImagesFiles");

            migrationBuilder.RenameTable(
                name: "FileTypes",
                newName: "FilesTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ImageFileDbo_SizeId",
                table: "ImagesFiles",
                newName: "IX_ImagesFiles_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageFileDbo_ImageId",
                table: "ImagesFiles",
                newName: "IX_ImagesFiles_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageFileDbo_FileId",
                table: "ImagesFiles",
                newName: "IX_ImagesFiles_FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagesFiles",
                table: "ImagesFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilesTypes",
                table: "FilesTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FilesTypes_TypeId",
                table: "Files",
                column: "TypeId",
                principalTable: "FilesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesFiles_Files_FileId",
                table: "ImagesFiles",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesFiles_ImagesSizes_SizeId",
                table: "ImagesFiles",
                column: "SizeId",
                principalTable: "ImagesSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesFiles_Images_ImageId",
                table: "ImagesFiles",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
