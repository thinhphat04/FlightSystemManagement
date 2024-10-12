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
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<FlightDocument> FlightDocuments { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserRole relationship
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserID, ur.RoleID });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleID);

            // FlightDocument relationship
            modelBuilder.Entity<FlightDocument>()
                .HasKey(fd => fd.FlightDocumentID);

            modelBuilder.Entity<FlightDocument>()
                .HasOne(fd => fd.Flight)
                .WithMany(f => f.FlightDocuments)
                .HasForeignKey(fd => fd.FlightID);

            modelBuilder.Entity<FlightDocument>()
                .HasOne(fd => fd.Document)
                .WithMany(d => d.FlightDocuments)
                .HasForeignKey(fd => fd.DocumentID);

            // Permission relationships
            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Document)
                .WithMany(d => d.Permissions)
                .HasForeignKey(p => p.DocumentID);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.PermissionGroup)
                .WithMany(pg => pg.Permissions)
                .HasForeignKey(p => p.PermissionGroupID);
        }
    }
}
