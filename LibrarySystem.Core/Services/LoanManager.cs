using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services;

public class LoanManager
{
  private int _activeLoansCount = 0;

  public void Borrow(Book book, Member member, DateTime borrowDate, DateTime dueDate)
  {
    _activeLoansCount++;
  }

  public int GetBorrowedBooksCount() => _activeLoansCount;
}