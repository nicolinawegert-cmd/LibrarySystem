using LibrarySystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repositories;

public class MemberRepository : IMemberRepository
{
  private readonly LibraryContext _context;

  public MemberRepository(LibraryContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Member>> GetAllAsync()
  {
    return await _context.Members
      .Include(m => m.Loans)
      .OrderBy(m => m.Name)
      .ToListAsync();
  }

  public async Task<Member?> GetByIdAsync(int id)
  {
    return await _context.Members
      .Include(m => m.Loans)
      .FirstOrDefaultAsync(m => m.Id == id);
  }

  public async Task AddAsync(Member member)
  {
    _context.Members.Add(member);
    await _context.SaveChangesAsync();
  }
}
