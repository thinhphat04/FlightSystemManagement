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
                    b.Property<int>("DocumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentID"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentID");

                    b.HasIndex("CreatorID");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Flight", b =>
                {
                    b.Property<int>("FlightID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightID"));

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFlightCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("PointOfLoading")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PointOfUnloading")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalDocuments")
                        .HasColumnType("int");

                    b.HasKey("FlightID");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.FlightDocument", b =>
                {
                    b.Property<int>("FlightDocumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightDocumentID"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DocumentID")
                        .HasColumnType("int");

                    b.Property<int>("FlightID")
                        .HasColumnType("int");

                    b.HasKey("FlightDocumentID");

                    b.HasIndex("DocumentID");

                    b.HasIndex("FlightID");

                    b.ToTable("FlightDocuments");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Permission", b =>
                {
                    b.Property<int>("PermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionID"));

                    b.Property<bool>("CanDownload")
                        .HasColumnType("bit");

                    b.Property<bool>("CanEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("CanView")
                        .HasColumnType("bit");

                    b.Property<int>("DocumentID")
                        .HasColumnType("int");

                    b.Property<int>("PermissionGroupID")
                        .HasColumnType("int");

                    b.HasKey("PermissionID");

                    b.HasIndex("DocumentID");

                    b.HasIndex("PermissionGroupID");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.PermissionGroup", b =>
                {
                    b.Property<int>("PermissionGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionGroupID"));

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PermissionGroupID");

                    b.ToTable("PermissionGroups");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.UserRole", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "RoleID");

                    b.HasIndex("RoleID");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Document", b =>
                {
                    b.HasOne("FlightSystemManagement.Entity.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.FlightDocument", b =>
                {
                    b.HasOne("FlightSystemManagement.Entity.Document", "Document")
                        .WithMany("FlightDocuments")
                        .HasForeignKey("DocumentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSystemManagement.Entity.Flight", "Flight")
                        .WithMany("FlightDocuments")
                        .HasForeignKey("FlightID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Permission", b =>
                {
                    b.HasOne("FlightSystemManagement.Entity.Document", "Document")
                        .WithMany("Permissions")
                        .HasForeignKey("DocumentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSystemManagement.Entity.PermissionGroup", "PermissionGroup")
                        .WithMany("Permissions")
                        .HasForeignKey("PermissionGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("PermissionGroup");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.UserRole", b =>
                {
                    b.HasOne("FlightSystemManagement.Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightSystemManagement.Entity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Document", b =>
                {
                    b.Navigation("FlightDocuments");

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Flight", b =>
                {
                    b.Navigation("FlightDocuments");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.PermissionGroup", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("FlightSystemManagement.Entity.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
