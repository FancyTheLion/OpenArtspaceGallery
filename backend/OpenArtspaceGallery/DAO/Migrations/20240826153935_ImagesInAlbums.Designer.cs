﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OpenArtspaceGallery.DAO.Contexts;

#nullable disable

namespace OpenArtspaceGallery.DAO.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20240826153935_ImagesInAlbums")]
    partial class ImagesInAlbums
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Albums.AlbumDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Files.FileDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginalName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.FilesTypes.FileTypeDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FileTypes");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Images.ImageDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AlbumId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Images.ImageFileDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SizeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("ImageId");

                    b.HasIndex("SizeId");

                    b.ToTable("ImageFileDbo");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Images.ImageSizeDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ImagesSizes");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Albums.AlbumDbo", b =>
                {
                    b.HasOne("OpenArtspaceGallery.DAO.Models.Albums.AlbumDbo", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Files.FileDbo", b =>
                {
                    b.HasOne("OpenArtspaceGallery.DAO.Models.FilesTypes.FileTypeDbo", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Images.ImageDbo", b =>
                {
                    b.HasOne("OpenArtspaceGallery.DAO.Models.Albums.AlbumDbo", "Album")
                        .WithMany("Images")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Images.ImageFileDbo", b =>
                {
                    b.HasOne("OpenArtspaceGallery.DAO.Models.Files.FileDbo", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OpenArtspaceGallery.DAO.Models.Images.ImageDbo", "Image")
                        .WithMany("Files")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OpenArtspaceGallery.DAO.Models.Images.ImageSizeDbo", "Size")
                        .WithMany()
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("Image");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Albums.AlbumDbo", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("OpenArtspaceGallery.DAO.Models.Images.ImageDbo", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
