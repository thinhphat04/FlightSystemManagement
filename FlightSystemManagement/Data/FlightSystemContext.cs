using FlightSystemManagement.Entity;
using Microsoft.EntityFrameworkCore;

namespace FlightSystemManagement.Data
{
    public class FlightSystemContext : DbContext
    {
        public FlightSystemContext(DbContextOptions<FlightSystemContext> options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<FlightDocument> FlightDocuments { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Entity configuration
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID); // Khóa chính cho bảng User

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255); // Bắt buộc và giới hạn độ dài email
            
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .IsRequired(); // Bắt buộc trường Role

            // FlightDocument relationship configuration
            modelBuilder.Entity<FlightDocument>()
                .HasKey(fd => fd.FlightDocumentID); // Khóa chính cho FlightDocument

            modelBuilder.Entity<FlightDocument>()
                .HasOne(fd => fd.Flight)
                .WithMany(f => f.FlightDocuments)
                .HasForeignKey(fd => fd.FlightID); // Khóa ngoại liên kết với bảng Flight

            modelBuilder.Entity<FlightDocument>()
                .HasOne(fd => fd.Document)
                .WithMany(d => d.FlightDocuments)
                .HasForeignKey(fd => fd.DocumentID); // Khóa ngoại liên kết với bảng Document

            // Permission relationships configuration
            modelBuilder.Entity<Permission>()
                .HasKey(p => p.PermissionID); // Khóa chính cho Permission

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Document)
                .WithMany(d => d.Permissions)
                .HasForeignKey(p => p.DocumentID); // Khóa ngoại liên kết với bảng Document

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.PermissionGroup)
                .WithMany(pg => pg.Permissions)
                .HasForeignKey(p => p.PermissionGroupID); // Khóa ngoại liên kết với PermissionGroup

            // PermissionGroup Entity configuration
            modelBuilder.Entity<PermissionGroup>()
                .HasKey(pg => pg.PermissionGroupID); // Khóa chính cho PermissionGroup

            // Document Entity configuration
            modelBuilder.Entity<Document>()
                .HasKey(d => d.DocumentID); // Khóa chính cho Document

            modelBuilder.Entity<Document>()
                .Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(255); // Bắt buộc trường Title và giới hạn độ dài

            modelBuilder.Entity<Document>()
                .Property(d => d.Type)
                .IsRequired()
                .HasMaxLength(100); // Bắt buộc trường Type và giới hạn độ dài

            modelBuilder.Entity<Document>()
                .Property(d => d.FilePath)
                .IsRequired(); // Đường dẫn file bắt buộc

            // Flight Entity configuration
            modelBuilder.Entity<Flight>()
                .HasKey(f => f.FlightID); // Khóa chính cho Flight

            modelBuilder.Entity<Flight>()
                .Property(f => f.FlightNumber)
                .IsRequired()
                .HasMaxLength(50); // Bắt buộc trường FlightNumber và giới hạn độ dài

            modelBuilder.Entity<Flight>()
                .Property(f => f.PointOfLoading)
                .IsRequired()
                .HasMaxLength(255); // Bắt buộc trường PointOfLoading và giới hạn độ dài

            modelBuilder.Entity<Flight>()
                .Property(f => f.PointOfUnloading)
                .IsRequired()
                .HasMaxLength(255); // Bắt buộc trường PointOfUnloading và giới hạn độ dài
        }
    }
}