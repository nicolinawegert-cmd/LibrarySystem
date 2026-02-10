using LibrarySystem.Core.Services;
using LibrarySystem.Core.Models;


namespace LibrarySystem.Core.Services
{
  public class Library
  {
    public BookCatalog Catalog { get; } = new();
    public MemberRegistry Members { get; } = new();
    public LoanManager Loans { get; } = new();
    public int GetTotalBooks()
    {
      return Catalog.Books.Count;
    }
    public int GetBorrowedBooksCount() => Loans.GetBorrowedBooksCount();
  }
}