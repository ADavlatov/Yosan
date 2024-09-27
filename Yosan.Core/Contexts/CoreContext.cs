using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yosan.Core.Models;

namespace Yosan.Core.Contexts;

public class CoreContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryUnit> CategoryUnits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryUnitConfiguration());
    }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("YosanCategories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).HasColumnName("User");
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.Type).HasColumnName("Type");
        builder.HasMany(x => x.Units).WithOne(x => x.Category);
    }
}

public class CategoryUnitConfiguration : IEntityTypeConfiguration<CategoryUnit>
{
    public void Configure(EntityTypeBuilder<CategoryUnit> builder)
    {
        builder.ToTable("YosanUnits");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).HasColumnName("User");
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.Sum).HasColumnName("Sum");
        builder.Property(x => x.Date).HasColumnName("Date");
        builder.HasOne(x => x.Category).WithMany(x => x.Units);
    }
}