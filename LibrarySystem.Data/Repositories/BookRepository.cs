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
      
    if (string.IsNullOrWhiteSpace(book.ISBN))
      throw new InvalidOperationException("ISBN is required.");

    _context.Books.Add(book);
    await _context.SaveChangesAsync();
  }

  public async Task<Book?> GetByISBNAsync(string isbn)
  {
    isbn = isbn.Trim();
    return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
  }

  public async Task<IEnumerable<Book>> GetAllAsync()
  {
    return await _context.Books.OrderBy(b => b.Title).ToListAsync();
  }

  public async Task<Book?> GetByIdAsync(int id)
  {
    return await _context.Books
    .Include(b => b.Loans)
    .ThenInclude(l => l.Member)
    .FirstOrDefaultAsync(b => b.Id == id);
  }

  public async Task<IEnumerable<Book>> SearchAsync(string searchTerm)
  {
    if (string.IsNullOrWhiteSpace(searchTerm))
      return await _context.Books.ToListAsync();

    var term = searchTerm.Trim().ToLower();

    return await _context.Books
      .Where(b =>
        b.Title.ToLower().Contains(term) ||
        b.Author.ToLower().Contains(term) ||
        b.ISBN.ToLower().Contains(term))
      .ToListAsync();
  }

  public async Task UpdateAsync(Book book)
  {
    var exists = await _context.Books.AnyAsync(b => b.Id == book.Id);
    if (!exists)
      throw new InvalidOperationException($"Book with ID '{book.Id}' was not found.");

    _context.Books.Update(book);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteAsync(int id)
  {
    var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
    if (book == null)
      throw new InvalidOperationException($"Book with ID '{id}' was not found.");

    _context.Books.Remove(book);
    await _context.SaveChangesAsync();
  }
}
