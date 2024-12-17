using Microsoft.EntityFrameworkCore;
using ScheduleApp.Domain.Entities;

namespace ScheduleApp.Infrastructure.Data;

public class ScheduleAppDbContext : DbContext
{
    public ScheduleAppDbContext(DbContextOptions<ScheduleAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id); 

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15); 
        });
    }
}
