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
}