using LibrarySystem.Core.Models;

namespace LibrarySystem.Data.Repositories;

public interface IMemberRepository
{
  Task<IEnumerable<Member>> GetAllAsync();
  Task<Member?> GetByIdAsync(int id);
  Task AddAsync(Member member);
}
