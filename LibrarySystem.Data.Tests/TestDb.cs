using LibrarySystem.Data;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data.Repositories;

namespace LibrarySystem.Data.Tests;

public class TestDb
{
  public static LibraryContext CreateContext(string? dbname = null)
  {
    dbname ??= Guid.NewGuid().ToString();

    var options = new DbContextOptionsBuilder<LibraryContext>()
      .UseInMemoryDatabase(dbname)
      .Options;

    return new LibraryContext(options);
  }

  public static BookRepository CreateBookRepository(LibraryContext context)
    => new BookRepository(context);
}