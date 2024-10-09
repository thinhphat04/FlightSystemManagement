using FlightSystemManagement.Entity;
using Microsoft.EntityFrameworkCore;

namespace FlightSystemManagement.Data
{
    public class FlightSystemContext : DbContext
    {
        public FlightSystemContext(DbContextOptions<FlightSystemContext> options) : base(options)
        {
        }

        // DbSets for entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightDocument> FlightDocuments { get; set; }

        // Permission-related entities
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<PermissionGroupAssignment> PermissionGroupAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for UserRole (combination of UserID and RoleID)
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserID, ur.RoleID });

            // UserRole relationships
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID)
                .OnDelete(DeleteBehavior.Cascade); // Set cascade delete if needed

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleID)
                .OnDelete(DeleteBehavior.Cascade);

            // FlightDocument relationships
            modelBuilder.Entity<FlightDocument>()
                .HasKey(fd => new { fd.FlightID, fd.DocumentID }); // Composite key for FlightDocument

            modelBuilder.Entity<FlightDocument>()
                .HasOne(fd => fd.Flight)
                .WithMany(f => f.FlightDocuments)
                .HasForeignKey(fd => fd.FlightID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FlightDocument>()
                .HasOne(fd => fd.Document)
                .WithMany(d => d.FlightDocuments)
                .HasForeignKey(fd => fd.DocumentID)
                .OnDelete(DeleteBehavior.Cascade);

            // Document relationships
            modelBuilder.Entity<Document>()
                .HasOne(d => d.DocumentType)
                .WithMany(dt => dt.Documents)
                .HasForeignKey(d => d.DocumentTypeID)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete on DocumentType

            modelBuilder.Entity<Document>()
                .HasOne(d => d.User)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UploadedBy)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletion when a User is deleted

            // PermissionGroup relationships
            modelBuilder.Entity<PermissionGroup>()
                .HasMany(pg => pg.Permissions)
                .WithOne(p => p.PermissionGroup)
                .HasForeignKey(p => p.PermissionGroupID)
                .OnDelete(DeleteBehavior.Cascade); // Set cascade delete if a PermissionGroup is deleted

            // PermissionGroupAssignment relationships
            modelBuilder.Entity<PermissionGroupAssignment>()
                .HasOne(pga => pga.PermissionGroup)
                .WithMany(pg => pg.PermissionGroupAssignments)
                .HasForeignKey(pga => pga.PermissionGroupID)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if the PermissionGroup is deleted

            modelBuilder.Entity<PermissionGroupAssignment>()
                .HasOne(pga => pga.Role)
                .WithMany(r => r.PermissionGroupAssignments)
                .HasForeignKey(pga => pga.RoleID)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if the Role is deleted

            // Composite key for PermissionGroupAssignment
            modelBuilder.Entity<PermissionGroupAssignment>()
                .HasKey(pga => new { pga.PermissionGroupID, pga.RoleID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
