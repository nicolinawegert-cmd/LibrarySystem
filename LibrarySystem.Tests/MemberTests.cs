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
}