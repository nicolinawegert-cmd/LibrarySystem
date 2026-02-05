using LibrarySystem.Core;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services;

public class MemberRegistry
{
  public List<Member> Members { get; } = new();

  public void Add(Member member)
  {
    Members.Add(member);
  }

  public Member? FindById(string memberId)
  {
    return Members.FirstOrDefault(m => m.MemberId == memberId);
  }
}