using LibrarySystem.Core.Services;

namespace LibrarySystem.Tests
{
  public class LibraryCompositionTests
  {
    [Fact]
    public void Library_ShouldExposeServices()
    {
      // Arrange & Act
      var library = new LibraryCompositionTests();

      // Assert
      Assert.NotNull(library.Catalog);
    }
  }
}