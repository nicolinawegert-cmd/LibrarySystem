using System.Security.Cryptography.X509Certificates;
using LibrarySystem.Core.Abstractions;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Models;

// Book class representing a book in the library system
public class Book : ISearchable
{ 
  public int Id { get; set; }

  public string ISBN { get; } = string.Empty;
  public string Title { get; private set; } = string.Empty;
  public string Author { get; private set; } = string.Empty;
  public int PublishedYear { get; private set; }
  public bool IsAvailable { get; private set; } = true;

  public ICollection<Loan> Loans { get; set; } = new List<Loan>();

  private Book() { } 

  // Initialize properties with constructor parameters
  public Book(string isbn, string title, string author, int publishedYear)
  {
    ISBN = isbn;
    Title = title;
    Author = author;
    PublishedYear = publishedYear;
  }
  public string GetInfo()
  {
    var status = IsAvailable ? "Tillgänglig" : "Utlånad";
    return $"{Title} av {Author} ({PublishedYear}) - {status}";
  }
 
  public void MarkAsBorrowed() => IsAvailable = false;
  public void MarkAsReturned() => IsAvailable = true;

  public bool Matches(string searchTerm)
  {
    if (string.IsNullOrWhiteSpace(searchTerm))
      return false;

    var term = searchTerm.Trim();

    return Author.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            Title.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            ISBN.Contains(term, StringComparison.OrdinalIgnoreCase);
  }
  
  public void UpdateTitle(string title) => Title = title;
}
