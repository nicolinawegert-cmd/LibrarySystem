using LibrarySystem.Core;

namespace LibrarySystem.Core.Models;

public class Loan
{
  public Book Book { get; }
  public Member Member { get; }
  public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
  {
    Book = book;
    Member = member;
  }
  public bool IsReturned => ReturnDate is not null;

  public DateTime? ReturnDate { get; private set; }

  public void MarkReturned(DateTime returnDate)
  {
    ReturnDate = returnDate;
  }
}
