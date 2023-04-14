using Microsoft.EntityFrameworkCore;
using WolfMail.Models.DataModels;

namespace WolfMail.Data;

/// <summary>
/// Represents a database context for WolfMail.
/// </summary>
public class WolfMailContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WolfMailContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the context.</param>
    public WolfMailContext(DbContextOptions<WolfMailContext> options)
        : base(options)
    {
        // Ensure that the database is created if it doesn't exist.
        Database.EnsureCreated();
    }

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types exposed in <see cref="DbSet{TEntity}"/> properties on this context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasOne(u => u.MailAddress)
            .WithOne(ma => ma.User)
            .HasForeignKey<WolfMailAddress>(ma => ma.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WolfMailAddress>().HasIndex(m => m.Email).IsUnique();
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> for <see cref="User"/> entities in the context.
    /// </summary>
    /// <value>The <see cref="DbSet{TEntity}"/> for <see cref="User"/> entities in the context.</value>
    public DbSet<User> Users { get; set; } = default!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> for <see cref="WolfMailAddress"/> entities in the context.
    /// </summary>
    /// <value>The <see cref="DbSet{TEntity}"/> for <see cref="WolfMailAddress"/> entities in the context.</value>
    public DbSet<WolfMailAddress> MailAddresses { get; set; } = default!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> for <see cref="MailGroup"/> entities in the context.
    /// </summary>
    /// <value>The <see cref="DbSet{TEntity}"/> for <see cref="MailGroup"/> entities in the context.</value>
    public DbSet<MailGroup> MailGroups { get; set; } = default!;
}
