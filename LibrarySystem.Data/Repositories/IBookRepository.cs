using LibrarySystem.Core.Models;

namespace LibrarySystem.Data.Repositories;

public interface IBookRepository
{
  Task AddAsync(Book book);
}