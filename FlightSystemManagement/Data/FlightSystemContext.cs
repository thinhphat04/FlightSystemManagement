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
        public DbSet<UserFlightAssignment> UserFlightAssignments { get; set; }
        public DbSet<FlightDocument> FlightDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite keys
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserID, ur.RoleID });

            modelBuilder.Entity<UserFlightAssignment>()
                .HasKey(ufa => new { ufa.UserID, ufa.FlightID });

            // Configure relationships
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleID);

            modelBuilder.Entity<UserFlightAssignment>()
                .HasOne(ufa => ufa.User)
                .WithMany(u => u.Assignments)
                .HasForeignKey(ufa => ufa.UserID);

            modelBuilder.Entity<UserFlightAssignment>()
                .HasOne(ufa => ufa.Flight)
                .WithMany(f => f.Assignments)
                .HasForeignKey(ufa => ufa.FlightID);

            modelBuilder.Entity<FlightDocument>()
                .HasOne(fd => fd.Flight)
                .WithMany(f => f.FlightDocuments)
                .HasForeignKey(fd => fd.FlightID);

            modelBuilder.Entity<FlightDocument>()
                .HasOne(fd => fd.Document)
                .WithMany(d => d.FlightDocuments)
                .HasForeignKey(fd => fd.DocumentID);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.DocumentType)
                .WithMany(dt => dt.Documents)
                .HasForeignKey(d => d.DocumentTypeID);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.User)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UploadedBy);

            base.OnModelCreating(modelBuilder);
        }
    }
}
