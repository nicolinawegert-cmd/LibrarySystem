using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests;

public class MemberTests
{
  [Fact]
  public void Constructor_ShouldSetMemberID()
  {
    // Arrange & Act
    var member = new Member("M001", "Testnamn", "test@example.com");

    // Assert
    Assert.Equal("M001", member.MemberID);
  }

  [Fact]
  public void Constructor_ShouldSetName()
  {
    // Arrange & Act
    var member = new Member("M001", "Testnamn", "test@example.com");

    // Assert
    Assert.Equal("Testnamn", member.Name);
  }

  [Fact]
  public void Constructor_ShouldSetEmail()
  {
    // Arrange & Act
    var member = new Member("M001", "Testnamn", "test@example.com");

    // Assert
    Assert.Equal("test@example.com", member.Email);
  }

  [Fact]
  public void Constructor_ShouldSetMemberSince_ToNow()
  {
    // Arrange & Act
    var before = DateTime.UtcNow;

    var member = new Member("M001", "Testnamn", "test@example.com");

    var after = DateTime.UtcNow;

    // Assert
    Assert.InRange(member.MemberSince, before, after);
  }

  [Fact]
  public void BorrowedBooks_ShouldBeEmpty_ForNewMember()
  {
    // Arrange & Act
    var member = new Member("M001", "Testnamn", "test@example.com");

    // Assert
    Assert.Empty(member.BorrowedBooks);
  }

  [Fact]
  public void AddBorrowedBook_ShouldAddBookToBorrowedBooks()
  {
    // Arrange
    var member = new Member("M001", "Testnamn", "test@example.com");
    var book = new Book("B001", "Testbok", "Testförfattare", 2020);

    // Act
    member.AddBorrowedBook(book);

    // Assert
    Assert.Single(member.BorrowedBooks);
    Assert.Equal("B001", member.BorrowedBooks[0].ISBN);
  }

  [Fact]
  public void RemoveBorrowedBook_ShouldRemoveBookFromBorrowedBooks()
  {
    // Arrange
    var member = new Member("M001", "Testnamn", "test@example.com");
    var book = new Book("B001", "Testbok", "Testförfattare", 2020);

    member.AddBorrowedBook(book);
    Assert.Single(member.BorrowedBooks);

    // Act
    member.RemoveBorrowedBook(book);

    // Assert
    Assert.Empty(member.BorrowedBooks);
  }

  [Fact]
  public void GetInfo_ShouldReturnFormattedMemberInfo()
  {
    // Arrange
    var member = new Member("M001", "Testnamn", "test@example.com");

    // Act
    var info = member.GetInfo();

    // Assert
    Assert.Contains("Testnamn", info);
    Assert.Contains("M001", info);
    Assert.Contains("test@example.com", info);
  }

  [Fact]
  public void GetInfo_ShouldIncludeBorrowedBookdCount()
  {
    // Arrange
    var member = new Member("M001", "Testnamn", "test@example.com");
    var book = new Book("B001", "Testbok 1", "Testförfattare", 2020);

    member.AddBorrowedBook(book);

    // Act
    var info = member.GetInfo();

    // Assert
    Assert.Contains("Lån: 1", info);
  }

  [Fact]
  public void GetInfo_ShouldIncludeMemberSinceDate()
  {
    // Arrange
    var member = new Member("M001", "Testnamn", "test@example.com");

    // Act
    var info = member.GetInfo();

    // Assert
    Assert.Contains("Medlem sedan:", info);
  }

}