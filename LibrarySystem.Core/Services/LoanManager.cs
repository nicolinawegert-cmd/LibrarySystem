using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services;

public class LoanManager
{
  private readonly List<Loan> _loans = new();
  public IReadOnlyList<Loan> ActiveLoans => _loans.Where(l => !l.IsReturned).ToList();

  public Loan Borrow(Book book, Member member, DateTime loanDate, DateTime dueDate)
  {
    book.MarkAsBorrowed();
    member.AddBorrowedBook(book);

    var loan = new Loan(book, member, loanDate, dueDate);
    _loans.Add(loan);

    return loan;
  }

  public int GetBorrowedBooksCount() => _loans.Count(l => !l.IsReturned);
}