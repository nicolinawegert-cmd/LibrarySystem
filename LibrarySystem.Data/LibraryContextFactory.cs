using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design; 

namespace LibrarySystem.Data;

public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
{
  public LibraryContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
    optionsBuilder.UseSqlite("Data Source=library.db");

    return new LibraryContext(optionsBuilder.Options);
  }
}