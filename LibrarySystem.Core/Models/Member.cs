using LibrarySystem.Core;

namespace LibrarySystem.Core.Models;

public class Member
{
  public string MemberID { get; }
  public string Name { get; }
  public string Email { get; }

  public DateTime MemberSince { get; }

  public List<Book> BorrowedBooks { get; } = new();

  public Member(string memberID, string name, string email)
  {
    MemberID = memberID;
    Name = name;
    Email = email;
    MemberSince = DateTime.UtcNow;
  }

  public void AddBorrowedBook(Book book)
  {
    BorrowedBooks.Add(book);
  }
  
  public void RemoveBorrowedBook(Book book)
  {
    BorrowedBooks.Remove(book);
  }
}