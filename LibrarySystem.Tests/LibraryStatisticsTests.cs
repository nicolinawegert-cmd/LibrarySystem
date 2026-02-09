using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using Xunit;

namespace LibrarySystem.Tests;

public class LibraryStatisticsTests
{
  [Fact]
  public void GetTotalBooks_ShouldReturnCorrectCount()
  {
    var library = new Library();
    library.Catalog.Add(new Book("1", "A", "X", 2000));
    library.Catalog.Add(new Book("2", "B", "Y", 2001));

    var total = library.GetTotalBooks();

    Assert.Equal(2, total);
  }
}
