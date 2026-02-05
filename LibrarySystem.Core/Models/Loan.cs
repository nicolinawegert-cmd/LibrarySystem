using LibrarySystem.Core;

namespace LibrarySystem.Core.Models;

public class Loan
{
  public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
  {

  }
  public bool IsReturned => ReturnDate is not null;

  public DateTime? ReturnDate { get; private set; }

  public void MarkReturned(DateTime returnDate)
  {
    ReturnDate = returnDate;
  }
}
