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

  public async Task UpdateAsync(Member member)
  {
    var exists = await _context.Members.AnyAsync(m => m.Id == member.Id);
    if (!exists)
      throw new InvalidOperationException($"Member with ID '{member.Id}' was not found.");

    _context.Members.Update(member);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteAsync(int id)
  {
    var member = await _context.Members
      .Include(m => m.Loans)
      .FirstOrDefaultAsync(m => m.Id == id);

    if (member is null)
      throw new InvalidOperationException($"Member with ID '{id}' was not found.");

    if (member.Loans.Any(l => !l.IsReturned))
      throw new InvalidOperationException("Member has active loans and cannot be deleted.");

    _context.Members.Remove(member);
    await _context.SaveChangesAsync();
  }
}
