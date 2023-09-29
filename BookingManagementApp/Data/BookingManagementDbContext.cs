using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BookingManagementDbContext : DbContext
    {
        public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options) { }

        // Add models to Migrate
        public DbSet<AccountRoles> AccountRoles { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Universities> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employees>().HasIndex(e => new
            {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();

            // One University has many Educations
            modelBuilder.Entity<Universities>()
                        .HasMany(e => e.Educations)
                        .WithOne(u => u.Universities)
                        .HasForeignKey(e => e.UniversityGuid);
                      

            //one education has one employee
            modelBuilder.Entity<Education>()
                .HasOne(e => e.Employees)
                .WithOne(e => e.Education)
                .HasForeignKey<Education>(e => e.Guid);
            

            // one employee has one account
            modelBuilder.Entity<Employees>()
                .HasOne(a => a.Accounts)
                .WithOne(e => e.Employees)
                .HasForeignKey<Employees>(e => e.Guid)
                .OnDelete(DeleteBehavior.Restrict);

            //one employee has many booking
            modelBuilder.Entity<Employees>()
                .HasMany(b => b.bookings)
                .WithOne(e => e.Employees)
                .HasForeignKey(b => b.EmployeeGuid);


            //many boking has one rooms
            modelBuilder.Entity<Bookings>()
                .HasOne(r => r.Rooms)
                .WithMany(b => b.Bookings)
                .HasForeignKey(r => r.RoomGuid);


            // one account has many account roles
            modelBuilder.Entity<Accounts>()
                .HasMany(ar => ar.AccountRoles)
                .WithOne(a => a.Accounts)
                .HasForeignKey(ar => ar.AccountGuid);


            //many account roles has one role
            modelBuilder.Entity<AccountRoles>()
                .HasOne(r => r.Roles)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(ar => ar.RoleGuid);
          


            

        }




    }
}
