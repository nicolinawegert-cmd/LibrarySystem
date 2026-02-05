using LibrarySystem.Core;

namespace LibrarySystem.Core.Models;

public class Member
{
  public string MemberID { get; }
  public string Name { get; }
  public string Email { get; }

  public DateTime MemberSince { get; } 

  public Member(string memberID, string name, string email)
  {
    MemberID = memberID;
    Name = name;
    Email = email;
    MemberSince = DateTime.UtcNow;
  }
}