using LibrarySystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Data;

public class LibraryContext : DbContext
{
  public DbSet<Book> Books => Set<Book>();
  public DbSet<Loan> Loans => Set<Loan>();
  public DbSet<Member> Members => Set<Member>();

  public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
      optionsBuilder.UseSqlite("Data Source=library.db");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Book>()
      .HasIndex(b => b.ISBN)
      .IsUnique();

    modelBuilder.Entity<Loan>()
      .HasOne(l => l.Book)
      .WithMany(b => b.Loans)
      .HasForeignKey(l => l.BookId);

    modelBuilder.Entity<Loan>()
      .HasOne(l => l.Member)
      .WithMany(m => m.Loans)
      .HasForeignKey(l => l.MemberId);
  }
}