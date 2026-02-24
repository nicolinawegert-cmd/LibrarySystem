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

        var book = new Book(isbn: "123", title: "Test Book", author: "Test Author", publicationYear: 2020);

        // Act
        await repository.AddAsync(book);

        // Assert
        var saved = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "123");
        Assert.NotNull(saved);
        Assert.Equal("Test Book", saved!.Title);
    }
}