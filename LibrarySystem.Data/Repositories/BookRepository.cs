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

  public async Task AddAsync(Book book)
  {
    var exists = await _context.Books.AnyAsync(b => b.ISBN == book.ISBN);
    if (exists)
      throw new InvalidOperationException($"A book with ISBN '{book.ISBN}' already exists.");

    _context.Books.Add(book);
    await _context.SaveChangesAsync();
  }
}