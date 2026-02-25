using LibrarySystem.Core.Models;

namespace LibrarySystem.Data.Repositories;

public class BookRepository : IBookRepository
{
  private readonly LibraryContext _context;

  public BookRepository(LibraryContext context)
  {
    _context = context;
  }

  public Task AddAsync(Book book) => throw new NotImplementedException();
}