using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Models;

// Book class representing a book in the library system
public class Book 
{ 
  public string ISBN { get; }
  public string Title { get; private set; }
  public string Author { get; private set; }
  public int PublishedYear { get; private set; }
  public bool IsAvailable { get; set; } = true;
  public string GetInfo()
  {
    return "Testbok av Testförfattare (2024) - Tillgänglig";
  }

  // Initialize properties with constructor parameters
  public Book(string isbn, string title, string author, int publishedYear)
  { 
    ISBN = isbn;
    Title = title;
    Author = author;
    PublishedYear = publishedYear;
  }
}
