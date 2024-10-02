using FlightSystemMangement.Entity;

using Microsoft.EntityFrameworkCore;

    public class FlightSystemContext : DbContext
    {
        // Constructor
        public FlightSystemContext(DbContextOptions<FlightSystemContext> options) : base(options)
        {
        }

        // DbSets for entities
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<FlightCrew> FlightCrew { get; set; }
        public DbSet<FlightDocument> FlightDocument { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set table names explicitly
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<DocumentType>().ToTable("DocumentType");
            modelBuilder.Entity<Permission>().ToTable("Permission");
            modelBuilder.Entity<Flight>().ToTable("Flight");
            modelBuilder.Entity<FlightCrew>().ToTable("FlightCrew");
            modelBuilder.Entity<FlightDocument>().ToTable("FlightDocument");

            // Configure UserRoles composite key
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // Configure UserRoles relationships
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Configure FlightCrew composite key
            modelBuilder.Entity<FlightCrew>()
                .HasKey(fc => new { fc.FlightId, fc.UserId });

            // Configure FlightCrew relationships
            modelBuilder.Entity<FlightCrew>()
                .HasOne(fc => fc.Flight)
                .WithMany(f => f.FlightCrews)
                .HasForeignKey(fc => fc.FlightId);

            modelBuilder.Entity<FlightCrew>()
                .HasOne(fc => fc.User)
                .WithMany(u => u.FlightCrews)
                .HasForeignKey(fc => fc.UserId);

            // Configure FlightDocuments relationships
            modelBuilder.Entity<FlightDocument>()
                .HasOne(fd => fd.Flight)
                .WithMany(f => f.FlightDocuments)
                .HasForeignKey(fd => fd.FlightId);

            modelBuilder.Entity<FlightDocument>()
                .HasOne(fd => fd.Document)
                .WithMany(d => d.FlightDocuments)
                .HasForeignKey(fd => fd.DocumentId);

            // Configure Permissions relationships
            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.DocumentType)
                .WithMany(dt => dt.Permissions)
                .HasForeignKey(p => p.DocumentTypeId);

            // Configure Documents relationships
            modelBuilder.Entity<Document>()
                .HasOne(d => d.DocumentType)
                .WithMany(dt => dt.Documents)
                .HasForeignKey(d => d.DocumentTypeId);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.User)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UploadedBy);

            base.OnModelCreating(modelBuilder);
        }
    }
