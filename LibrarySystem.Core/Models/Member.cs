using LibrarySystem.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Core.Models;

public class Member
{
  public int Id { get; set; }

  public string MemberId { get; private set; } = string.Empty;
  public string Name { get; private set; } = string.Empty;
  public string Email { get; private set; } = string.Empty;

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

  public string GetInfo()
  {
    return $"{Name} ({MemberId}) - {Email} | Medlem sedan: {MemberSince:yyyy-MM-dd} | Lån: {Loans.Count}";
  }
}