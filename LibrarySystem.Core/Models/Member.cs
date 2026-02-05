using LibrarySystem.Core;

namespace LibrarySystem.Core.Models;

public class Member
{
  public string MemberId { get; }
  public string Name { get; }
  public string Email { get; }

  public DateTime MemberSince { get; }

  public Member(string memberId, string name, string email)
  {
    MemberId = memberId;
    Name = name;
    Email = email;
    MemberSince = DateTime.UtcNow;
  }

  private readonly List<Book> _borrowedBooks = new();
  public IReadOnlyList<Book> BorrowedBooks => _borrowedBooks;
  public void AddBorrowedBook(Book book)
  {
    _borrowedBooks.Add(book);
  }

  public void RemoveBorrowedBook(Book book)
  {
    _borrowedBooks.Remove(book);
  }

  public string GetInfo()
  {
    return $"{Name} ({MemberId}) - {Email} | Medlem sedan: {MemberSince:yyyy-MM-dd} | Lån: {BorrowedBooks.Count}";
  }
}