using LibrarySystem.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Core.Models;

public class Member
{
  public int Id { get; set; }

  public string MemberId { get; } = string.Empty;
  public string Name { get; } = string.Empty;
  public string Email { get; } = string.Empty;

  public DateTime MemberSince { get; private set; }

  public ICollection<Loan> Loans { get; set; } = new List<Loan>();

  private Member() { }

  public Member(string memberId, string name, string email)
  {
    MemberId = memberId;
    Name = name;
    Email = email;
    MemberSince = DateTime.UtcNow;
  }

  private readonly List<Book> _borrowedBooks = new();
  public IReadOnlyList<Book> BorrowedBooks => _borrowedBooks;
  public void AddBorrowedBook(Book book) => _borrowedBooks.Add(book);
  public void RemoveBorrowedBook(Book book) => _borrowedBooks.Remove(book);

  public string GetInfo()
  {
    return $"{Name} ({MemberId}) - {Email} | Medlem sedan: {MemberSince:yyyy-MM-dd} | Lån: {BorrowedBooks.Count}";
  }
}