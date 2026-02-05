using LibrarySystem.Core;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services;

public class MemberRegistry
{
  private readonly List<Member> _members = new();
  public IReadOnlyList<Member> Members => _members;

  public void Add(Member member)
  {
    _members.Add(member);
  }

  public Member? FindById(string memberId)
  {
    return _members.FirstOrDefault(m => string.Equals(m.MemberId, memberId, StringComparison.OrdinalIgnoreCase));
  }  
}
