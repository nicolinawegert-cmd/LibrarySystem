using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Models;

public class Book
{
  public string ISBN { get; }
  public Book(string isbn, string title, string author, int publishedYear)
  {
    ISBN = isbn;

  }
}
