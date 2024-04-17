using CalendarBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendarBooking.Context;

public class CalendarContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }

    public CalendarContext(DbContextOptions<CalendarContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.ToTable("appointments");

            entity.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");
            entity.Property(e => e.Day).HasColumnName("Day");
            entity.Property(e => e.Month).HasColumnName("Month");
            entity.Property(e => e.Hour).HasColumnName("Hour");
            entity.Property(e => e.Min).HasColumnName("Min");

        });
        
        base.OnModelCreating(modelBuilder);
    }
}