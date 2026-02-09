using LibrarySystem.Core.Models;
using System.Linq;

namespace LibrarySystem.Core.Services;

public class BookCatalog
{
  private readonly List<Book> _books = new();
  public IReadOnlyList<Book> Books => _books;

  public void Add(Book book)
  {
    _books.Add(book);
  }
  public IEnumerable<Book> Search(string term)
  {
    return _books.Where(b => b.Matches(term));
  }

  public IEnumerable<Book> SortByTitle()
  {
    return _books.OrderBy(b => b.Title, StringComparer.OrdinalIgnoreCase);
  }
}