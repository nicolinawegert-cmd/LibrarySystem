using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services;

public class LoanManager
{
  private readonly List<Loan> _loans = new();
  public IReadOnlyList<Loan> ActiveLoans => _loans.Where(l => !l.IsReturned).ToList();
  public IReadOnlyList<Loan> Loans => _loans;

  public Loan Borrow(Book book, Member member, DateTime loanDate, DateTime dueDate)
  {
    if (book is null) throw new ArgumentNullException(nameof(book));
    if (member is null) throw new ArgumentNullException(nameof(member));

    if (dueDate <= loanDate) 
      throw new ArgumentException("Due date must be after loan date.");

    if (!book.IsAvailable) 
      throw new InvalidOperationException("Book is not available for borrowing.");

    book.MarkAsBorrowed();
    member.AddBorrowedBook(book);

    var loan = new Loan(book, member, loanDate, dueDate);
    _loans.Add(loan);

    return loan;
  }

  public int GetBorrowedBooksCount() => _loans.Count(l => !l.IsReturned);
}