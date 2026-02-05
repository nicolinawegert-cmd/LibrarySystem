using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services;

public class BookCatalog
{
  private readonly List<Book> _books = new();
  public IReadOnlyList<Book> Books => _books;

  public void Add(Book book)
  {
    _books.Add(book);
  }
}