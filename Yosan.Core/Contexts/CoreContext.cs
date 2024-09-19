using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yosan.Core.Models;

namespace Yosan.Core.Contexts;

public class CoreContext : DbContext
{
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Saving> Savings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new IncomeConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new SavingConfiguration());
    }
}

public class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.ToTable("YosanIncomes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.Sum).HasColumnName("Sum");
        builder.Property(x => x.Date).HasColumnName("Date");
    }
}

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("YosanExpenses");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.Sum).HasColumnName("Sum");
        builder.Property(x => x.Date).HasColumnName("Date");
    }
}

public class SavingConfiguration : IEntityTypeConfiguration<Saving>
{
    public void Configure(EntityTypeBuilder<Saving> builder)
    {
        builder.ToTable("YosanSavings");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.Sum).HasColumnName("Sum");
        builder.Property(x => x.Date).HasColumnName("Date");
    }
}