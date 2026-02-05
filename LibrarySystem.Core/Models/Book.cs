using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Models;

public class Book
{
  public string ISBN { get; }
  public string Title { get; }
  public string Author { get; }
  public int PublishedYear { get; }
  public Book(string isbn, string title, string author, int publishedYear)
  {
    ISBN = isbn;
    Title = title;
    Author = author;
    PublishedYear = publishedYear;
  }
}
