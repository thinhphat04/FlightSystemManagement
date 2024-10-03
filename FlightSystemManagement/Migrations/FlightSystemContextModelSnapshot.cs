﻿// <auto-generated />
using System;
using FlightSystemManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightSystemManagement.Migrations
{
    [DbContext(typeof(FlightSystemContext))]
    partial class FlightSystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlightSystemManagement.Entity.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UploadedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UploadedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("UploadedBy");

                    b.ToTable("Document", (string)null);
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DocumentType", (string)null);
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FlightDestination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightOrigin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Flight", (string)null);
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.FlightCrew", b =>
                {
                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("FlightId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("FlightCrew", (string)null);
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.FlightDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DownloadedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDownloaded")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("FlightId");

                    b.ToTable("FlightDocument", (string)null);
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("CanCreate")
                        .HasColumnType("bit");

                    b.Property<bool>("CanDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("CanUpdate")
                        .HasColumnType("bit");

                    b.Property<bool>("CanView")
                        .HasColumnType("bit");

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("RoleId");

                    b.ToTable("Permission", (string)null);
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentCustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ResetPasswordOtp")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ResetPasswordOtpExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Wishlist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Document", b =>
                {
                    b.HasOne("FlightSystemManagement.Entity.DocumentType", "DocumentType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSystemManagement.Entity.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UploadedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.FlightCrew", b =>
                {
                    b.HasOne("FlightSystemManagement.Entity.Flight", "Flight")
                        .WithMany("FlightCrews")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSystemManagement.Entity.User", "User")
                        .WithMany("FlightCrews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.FlightDocument", b =>
                {
                    b.HasOne("FlightSystemManagement.Entity.Document", "Document")
                        .WithMany("FlightDocuments")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSystemManagement.Entity.Flight", "Flight")
                        .WithMany("FlightDocuments")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Permission", b =>
                {
                    b.HasOne("FlightSystemManagement.Entity.DocumentType", "DocumentType")
                        .WithMany("Permissions")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSystemManagement.Entity.Role", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.UserRole", b =>
                {
                    b.HasOne("FlightSystemManagement.Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSystemManagement.Entity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Document", b =>
                {
                    b.Navigation("FlightDocuments");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.DocumentType", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Flight", b =>
                {
                    b.Navigation("FlightCrews");

                    b.Navigation("FlightDocuments");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Role", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.User", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("FlightCrews");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}