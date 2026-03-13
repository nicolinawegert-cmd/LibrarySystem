using LibrarySystem.Core;

namespace LibrarySystem.Core.Models;

public class Loan
{
  public int Id { get; set; }
  
  public int BookId { get; private set; }
  public int MemberId { get; private set; }

  public Book Book { get; private set; } = default!;
  public Member Member { get; private set; } = default!;

  public DateTime LoanDate { get; private set; }
  public DateTime DueDate { get; private set; }

  public DateTime? ReturnDate { get; private set; }
  public bool IsReturned => ReturnDate is not null;
  public bool IsOverdue => !IsReturned && DateTime.UtcNow.Date > DueDate.Date;

  private Loan() { }

  public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
  {
    Book = book ?? throw new ArgumentNullException(nameof(book));
    Member = member ?? throw new ArgumentNullException(nameof(member));
    LoanDate = loanDate;
    DueDate = dueDate;
  }

  public void MarkReturned(DateTime returnDate)
  {
    ReturnDate = returnDate;
  }

  public void UpdateDueDate(DateTime dueDate)
  {
    DueDate = dueDate;
  }
}
