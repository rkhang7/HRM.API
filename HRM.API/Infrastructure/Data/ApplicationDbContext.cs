using HRM.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRM.API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommomEntity>()
                .HasIndex(e => e.Code)
                .IsUnique();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleEntity>()
               .HasIndex(e => e.Type)
               .IsUnique();
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<UserEntity>()
            //   .HasIndex(e => e.UserName)
            //   .IsUnique();
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasIndex(user => new { user.UserName, user.Email, user.PhoneNumber })
               .IsUnique();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CommomEntity> Commoms { get; set; }
        public DbSet<RoleEntity> Role { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<LogEntity> Logs { get; set; }



        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<MasterEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.CreatedAt).IsModified = false; // Không cập nhật CreatedAt
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
