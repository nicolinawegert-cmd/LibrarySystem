using System.Security.Cryptography.X509Certificates;
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
    var status = IsAvailable ? "Tillgänglig" : "Utlånad";
    return $"{Title} av {Author} ({PublishedYear}) - {status}";
  }

  // Initialize properties with constructor parameters
  public Book(string isbn, string title, string author, int publishedYear)
  {
    ISBN = isbn;
    Title = title;
    Author = author;
    PublishedYear = publishedYear;
  }

  internal void MarkAsBorrowed()
  {
    IsAvailable = false;
  }

  internal void MarkAsReturned()
  {
    IsAvailable = true;
  }
  
  public bool Matches(string searchTerm)
  {
    return Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
  }
}
