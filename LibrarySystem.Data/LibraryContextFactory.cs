using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design; 

namespace LibrarySystem.Data;

public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
{
  public LibraryContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();

    var databasePath = Path.GetFullPath(
        Path.Combine(Directory.GetCurrentDirectory(), "..", "LibrarySystem.App", "library.db"));

    optionsBuilder.UseSqlite($"Data Source={databasePath}");

    return new LibraryContext(optionsBuilder.Options);
  }

}