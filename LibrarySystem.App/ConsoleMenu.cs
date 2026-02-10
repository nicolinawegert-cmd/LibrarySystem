using LibrarySystem.Core.Services;
using LibrarySystem.Core.Models;

public class ConsoleMenu
{
  private readonly Library _library;

  public ConsoleMenu(Library library)
  {
    _library = library;
  }

  public void Run()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("=== Bibliotekssystem ===");
      Console.WriteLine("1. Visa alla böcker");
      Console.WriteLine("2. Sök bok");
      Console.WriteLine("3. Låna bok");
      Console.WriteLine("4. Returnera bok");
      Console.WriteLine("5. Visa medlemmar");
      Console.WriteLine("6. Statistik");
      Console.WriteLine("0. Avsluta");
      Console.Write("\nVälj ett alternativ: ");

      var choice = Console.ReadLine();

      switch (choice)
      {
        case "1": ShowAllBooks(); break;
        case "2": SearchBooks(); break;
        case "3": BorrowBook(); break;
        case "4": ReturnBook(); break;
        case "5": ShowMembers(); break;
        case "6": ShowStatistics(); break;
        case "0": return;
        default:
          Console.WriteLine("Ogiltigt val. Tryck på valfri tangent för att fortsätta...");
          Pause();
          break;
      }
    }
  }
  private void ShowAllBooks()
  {
    Console.Clear();
    Console.WriteLine("=== Alla Böcker ===");

    foreach (var book in _library.Catalog.Books)
      Console.WriteLine(book.GetInfo());

    Pause();
  }
  public void SearchBooks()
  {
    Console.Clear();
    Console.WriteLine("=== Sök Bok ===");
    Console.Write("Ange titel eller författare: ");
    var query = Console.ReadLine();

    var results = _library.Catalog.Books
      .Where(b => b.Title.Contains(query ?? string.Empty, StringComparison.OrdinalIgnoreCase) ||
                  b.Author.Contains(query ?? string.Empty, StringComparison.OrdinalIgnoreCase))
      .ToList();

    Console.WriteLine("\nSökresultat:");
    if (!results.Any())
    {
      Console.WriteLine("Inga böcker matchade söktermen.");
      Pause();
      return;
    }
    Console.WriteLine($"Hittade {results.Count} böcker:");
    foreach (var book in results)
      Console.WriteLine(book.GetInfo());

    Pause();
  }
  private void BorrowBook()
  {
    Console.Clear();

    Console.Write("Ange ISBN: ");
    var isbn = (Console.ReadLine() ?? "").Trim();

    Console.Write("Ange medlems-ID: ");
    var memberId = (Console.ReadLine() ?? "").Trim();

    var book = _library.Catalog.Books.FirstOrDefault(b => b.ISBN == isbn);
    var member = _library.Members.FindById(memberId);

    if (book is null)
    {
      Console.WriteLine("Boken hittades inte.");
      Pause();
      return;
    }

    if (member is null)
    {
      Console.WriteLine("Medlemmen hittades inte.");
      Pause();
      return;
    }

    if (!book.IsAvailable)
    {
      Console.WriteLine("Boken är redan utlånad.");
      Pause();
      return;
    }

    var dueDate = DateTime.Today.AddDays(14);
    _library.Loans.Borrow(book, member, DateTime.Today, dueDate);

    Console.WriteLine($"\nBoken \"{book.Title}\" lånades ut till {member.Name}.");
    Console.WriteLine($"Återlämningsdatum: {dueDate:yyyy-MM-dd}");
    Pause();
  }
  private void ReturnBook()
  {
    Console.Clear();
    Console.Write("Ange ISBN för boken som returneras: ");
    var isbn = (Console.ReadLine() ?? "").Trim();

    var loan = _library.Loans.ActiveLoans.FirstOrDefault(l => l.Book.ISBN == isbn);

    if (loan is null)
    {
      Console.WriteLine("Ingen aktiv utlåning hittades för ISBN.");
      Pause();
      return;
    }
    loan.MarkReturned(DateTime.Today);

    loan.Book.MarkAsReturned();
    loan.Member.RemoveBorrowedBook(loan.Book);

    Console.WriteLine($"Boken \"{loan.Book.Title}\" har returnerats.");
    Pause();
  }
  private void ShowMembers()
  {
    Console.Clear();
    Console.WriteLine("=== Medlemmar ===\n");

    // Om du har registry.Members:
    foreach (var member in _library.Members.Members)
      Console.WriteLine(member.GetInfo());

    Pause();
  }

  private void ShowStatistics()
  {
    Console.Clear();
    Console.WriteLine("=== Statistik ===\n");

    Console.WriteLine($"Totalt antal böcker: {_library.GetTotalBooks()}");
    Console.WriteLine($"Antal utlånade böcker: {_library.GetBorrowedBooksCount()}");

    var mostActive = _library.GetMostActiveBorrower();
    Console.WriteLine($"Mest aktiv låntagare: {mostActive?.Name ?? "Ingen"}");

    Pause();
  }

  private static void Pause()
  {
    Console.WriteLine("\nTryck valfri tangent för att fortsätta...");
    Console.ReadKey();
  }
}