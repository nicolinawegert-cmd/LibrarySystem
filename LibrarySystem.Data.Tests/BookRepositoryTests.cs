using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Data.Repositories;
using LibrarySystem.Core.Models;
using Xunit;

public class BookRepositoryTests
{
    [Fact]
    public async Task AddAsync_ShouldSaveBookToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase("AddAsync_ShouldSaveBookToDatabase")
            .Options;

        using var context = new LibraryContext(options);
        var repository = new BookRepository(context);

        var book = new Book(isbn: "123", title: "Test Book", author: "Test Author", publishedYear: 2020);

        // Act
        await repository.AddAsync(book);

        // Assert
        var saved = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "123");
        Assert.NotNull(saved);
        Assert.Equal("Test Book", saved!.Title);
    }

    [Fact]
    public async Task AddAsync_ShouldThrow_WhenISBNAlreadyExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase("AddAsync_ShouldThrow_WhenISBNAlreadyExists")
            .Options;

        using var context = new LibraryContext(options);
        var repository = new BookRepository(context);

        var book1 = new Book(isbn: "123", title: "Test Book 1", author: "Test Author", publishedYear: 2020);
        var book2 = new Book(isbn: "123", title: "Test Book 2", author: "Test Author", publishedYear: 2021);

        await repository.AddAsync(book1);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => repository.AddAsync(book2));
        Assert.Contains("ISBN", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
}