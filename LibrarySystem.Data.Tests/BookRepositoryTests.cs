using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Data.Repositories;
using LibrarySystem.Core.Models;
using Xunit;
using LibrarySystem.Data.Tests;

public class BookRepositoryTests
{
    [Fact]
    public async Task AddAsync_ShouldSaveBookToDatabase()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(AddAsync_ShouldSaveBookToDatabase));
        var repository = TestDb.CreateBookRepository(context);

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
        using var context = TestDb.CreateContext(nameof(AddAsync_ShouldThrow_WhenISBNAlreadyExists));
        var repository = TestDb.CreateBookRepository(context);

        var book1 = new Book(isbn: "123", title: "Test Book 1", author: "Test Author", publishedYear: 2020);
        var book2 = new Book(isbn: "123", title: "Test Book 2", author: "Test Author", publishedYear: 2021);

        await repository.AddAsync(book1);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => repository.AddAsync(book2));
        Assert.Contains("ISBN", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task AddAsync_ShouldThrow_WhenISBNIsEmpty()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(AddAsync_ShouldThrow_WhenISBNIsEmpty));
        var repository = TestDb.CreateBookRepository(context);

        var book = new Book("", "Test Book", "Test Author", 2020);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => repository.AddAsync(book));
    }

    [Fact]
    public async Task GetByISBN_ShouldReturnBook_WhenExists()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(GetByISBN_ShouldReturnBook_WhenExists));
        var repository = TestDb.CreateBookRepository(context);

        var book = new Book(isbn: "123", title: "Test Book", author: "Test Author", publishedYear: 2020);
        await repository.AddAsync(book);

        // Act
        var result = await repository.GetByISBNAsync("123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Book", result!.Title);
    }

    [Fact]
    public async Task GetByISBNAsync_ShouldTrimIsbnInput()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(GetByISBNAsync_ShouldTrimIsbnInput));
        var repository = TestDb.CreateBookRepository(context);

        await repository.AddAsync(new Book(isbn: "ABC-123", title: "Test Book", author: "Test Author", publishedYear: 2020));

        // Act
        var result = await repository.GetByISBNAsync("  ABC-123  ");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Book", result!.Title);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllBooks()
    {
        // Arrange 
        using var context = TestDb.CreateContext(nameof(GetAllAsync_ShouldReturnAllBooks));
        var repository = TestDb.CreateBookRepository(context);

        await repository.AddAsync(new Book(isbn: "123", title: "Test Book 1", author: "Test Author", publishedYear: 2020));
        await repository.AddAsync(new Book(isbn: "456", title: "Test Book 2", author: "Test Author", publishedYear: 2021));

        // Act
        var books = (await repository.GetAllAsync()).ToList();

        // Assert
        Assert.Equal(2, books.Count);
        Assert.Contains(books, b => b.ISBN == "123");
        Assert.Contains(books, b => b.ISBN == "456");
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnBooksOrderedByTitle()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(GetAllAsync_ShouldReturnBooksOrderedByTitle));
        var repository = TestDb.CreateBookRepository(context);

        await repository.AddAsync(new Book("2", "B Title", "Author", 2020));
        await repository.AddAsync(new Book("1", "A Title", "Author", 2020));

        // Act
        var books = (await repository.GetAllAsync()).ToList();

        // Assert
        Assert.Equal("A Title", books[0].Title);
        Assert.Equal("B Title", books[1].Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnBook_WhenExists()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(GetByIdAsync_ShouldReturnBook_WhenExists));
        var repository = TestDb.CreateBookRepository(context);

        var book = new Book(isbn: "123", title: "Test Book", author: "Test Author", publishedYear: 2020);
        await repository.AddAsync(book);

        var expectedId = book.Id;

        // Act
        var result = await repository.GetByIdAsync(expectedId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("123", result!.ISBN);
        Assert.Equal("Test Book", result.Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        // Arrange 
        using var context = TestDb.CreateContext(nameof(GetByIdAsync_ShouldReturnNull_WhenNotFound));
        var repository = TestDb.CreateBookRepository(context);

        // Act
        var result = await repository.GetByIdAsync(12345);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SearchAsync_ShouldFindBooksByTitle()
    {
        // Arrange 
        using var context = TestDb.CreateContext(nameof(SearchAsync_ShouldFindBooksByTitle));
        var repository = TestDb.CreateBookRepository(context);

        await repository.AddAsync(new Book(isbn: "111", title: "Sagan om ringen", author: "J.R.R. Tolkien", publishedYear: 1954));
        await repository.AddAsync(new Book(isbn: "222", title: "1984", author: "George Orwell", publishedYear: 1949));

        // Act
        var results = (await repository.SearchAsync("ringen")).ToList();

        // Assert
        Assert.Single(results);
        Assert.Equal("111", results[0].ISBN);
    }

    [Fact]
    public async Task SearchAsync_ShouldFindBooksByAuthor()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(SearchAsync_ShouldFindBooksByAuthor));
        var repository = TestDb.CreateBookRepository(context);

        await repository.AddAsync(new Book(isbn: "111", title: "Sagan om ringen", author: "J.R.R. Tolkien", publishedYear: 1954));
        await repository.AddAsync(new Book(isbn: "222", title: "Hobbiten", author: "J.R.R. Tolkien", publishedYear: 1937));
        await repository.AddAsync(new Book(isbn: "333", title: "Animal Farm", author: "George Orwell", publishedYear: 1945));

        // Act
        var results = (await repository.SearchAsync("tolkien")).ToList();

        // Assert
        Assert.Equal(2, results.Count);
        Assert.All(results, b => Assert.Equal("J.R.R. Tolkien", b.Author));
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnAllBooks_WhenSearchTermIsEmpty()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(SearchAsync_ShouldReturnAllBooks_WhenSearchTermIsEmpty));
        var repository = TestDb.CreateBookRepository(context);

        await repository.AddAsync(new Book(isbn: "111", title: "Sagan om ringen", author: "J.R.R. Tolkien", publishedYear: 1954));
        await repository.AddAsync(new Book(isbn: "222", title: "1984", author: "George Orwell", publishedYear: 1949));

        // Act
        var results = (await repository.SearchAsync("")).ToList();

        // Assert
        Assert.Equal(2, results.Count);
    }

    [Fact]
    public async Task UpdateAsync_ShouldPersistChanges()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(UpdateAsync_ShouldPersistChanges));
        var repository = TestDb.CreateBookRepository(context);

        var book = new Book(isbn: "123", title: "Original Title", author: "Test Author", publishedYear: 2020);
        await repository.AddAsync(book);

        book.UpdateTitle("New Title");

        // Act
        await repository.UpdateAsync(book);

        // Assert
        var saved = await context.Books.FirstAsync(b => b.Id == book.Id);
        Assert.Equal("New Title", saved.Title);

    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveBook_WhenExists()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(DeleteAsync_ShouldRemoveBook_WhenExists));
        var repository = TestDb.CreateBookRepository(context);

        var book = new Book("555", "To Be Deleted", "Test Author", 2020);
        await repository.AddAsync(book);

        // Act
        await repository.DeleteAsync(book.Id);

        // Assert
        var exists = await context.Books.AnyAsync(b => b.Id == book.Id);
        Assert.False(exists);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrow_WhenBookNotFound()
    {
        // Arrange
        using var context = TestDb.CreateContext(nameof(DeleteAsync_ShouldThrow_WhenBookNotFound));
        var repository = TestDb.CreateBookRepository(context);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => repository.DeleteAsync(9999));
    }
}