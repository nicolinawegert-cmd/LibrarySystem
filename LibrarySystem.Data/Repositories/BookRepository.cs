using LibrarySystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repositories;

public class BookRepository : IBookRepository
{
  private readonly LibraryContext _context;

  public BookRepository(LibraryContext context)
  {
    _context = context;
  }

  public Task AddAsync(Book book)
  {
    _context.Books.Add(book);
    return _context.SaveChangesAsync();
  }
}