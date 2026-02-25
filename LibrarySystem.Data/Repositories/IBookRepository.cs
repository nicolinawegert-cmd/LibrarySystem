using LibrarySystem.Core.Models;

namespace LibrarySystem.Data.Repositories;

public interface IBookRepository
{
  Task AddAsync(Book book);
  Task<Book?> GetByISBNAsync(string isbn);
  Task<IEnumerable<Book>> GetAllAsync();
  Task<Book?> GetByIdAsync(int id);
  Task<IEnumerable<Book>> SearchAsync(string searchTerm);
}