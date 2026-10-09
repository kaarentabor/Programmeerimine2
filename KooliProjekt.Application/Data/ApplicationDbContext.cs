using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Agent> Agents { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<FileAttachment> FileAttachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>().Property(x => x.Summary).HasPrecision(18, 2);
        modelBuilder.Entity<Transaction>().Property(x => x.TotalPrice).HasPrecision(18, 2);
        modelBuilder.Entity<Transaction>().Property(x => x.PaidAmount).HasPrecision(18, 2);
        modelBuilder.Entity<Payment>().Property(x => x.Amount).HasPrecision(18, 2);

        // SQL Server ei luba mitut kaskaadkustutamise teed sama tabelini.
        modelBuilder.Entity<Registration>()
            .HasOne(x => x.Event).WithMany(x => x.Registrations)
            .HasForeignKey(x => x.EventId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Event).WithMany(x => x.Payments)
            .HasForeignKey(x => x.EventId).OnDelete(DeleteBehavior.NoAction);
    }
}
