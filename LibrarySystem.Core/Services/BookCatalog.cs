using LibrarySystem.Core;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services
{
  public class BookCatalog
  {
    public List<Book> Books { get; } = new();

    public void Add(Book book)
    {
      Books.Add(book);
    }
  }
}
