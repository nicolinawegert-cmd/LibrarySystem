using LibrarySystem.Core;

namespace LibrarySystem.Core.Models;

public class Member
{
  public string MemberID { get; }

  public Member(string memberID, string name, string email)
  {
    MemberID = memberID;
  }
}