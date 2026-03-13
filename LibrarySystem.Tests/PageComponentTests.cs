using Bunit;
using LibrarySystem.Core.Models;
using LibrarySystem.Data;
using LibrarySystem.Data.Repositories;
using LibrarySystem.Data.Services;
using LibrarySystem.Web.Components.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.Tests;

public class PageComponentTests : TestContext
{
  [Fact]
  public void BooksPage_ShouldRenderSearchControlsAndBookData()
  {
    // Arrange
    using var context = CreateContext(nameof(BooksPage_ShouldRenderSearchControlsAndBookData));
    context.Books.Add(new Book("123", "Ren testbok", "Testförfattare", 2024));
    context.SaveChanges();

    Services.AddScoped(_ => context);
    Services.AddScoped<IBookRepository, BookRepository>();

    // Act
    var cut = RenderComponent<Books>();

    // Assert
    Assert.Contains("Sök bok", cut.Markup);
    Assert.Contains("Sök", cut.Markup);
    Assert.Contains("Visa alla", cut.Markup);
    Assert.Contains("Ren testbok", cut.Markup);
    Assert.Contains("Testförfattare", cut.Markup);
  }

  [Fact]
  public void MembersPage_ShouldShowValidationErrors_WhenSubmittedEmpty()
  {
    // Arrange
    using var context = CreateContext(nameof(MembersPage_ShouldShowValidationErrors_WhenSubmittedEmpty));
    Services.AddScoped(_ => context);
    Services.AddScoped<IMemberRepository, MemberRepository>();

    // Act
    var cut = RenderComponent<Members>();
    cut.Find("form").Submit();

    // Assert
    cut.WaitForAssertion(() =>
    {
      Assert.Contains("Medlemsnummer är obligatoriskt.", cut.Markup);
      Assert.Contains("Namn är obligatoriskt.", cut.Markup);
      Assert.Contains("E-post är obligatorisk.", cut.Markup);
    });
  }

  [Fact]
  public void LoansPage_ShouldShowEmptyState_WhenThereAreNoActiveLoans()
  {
    // Arrange
    using var context = CreateContext(nameof(LoansPage_ShouldShowEmptyState_WhenThereAreNoActiveLoans));
    Services.AddScoped(_ => context);
    Services.AddScoped<LoanService>();

    // Act
    var cut = RenderComponent<Loans>();

    // Assert
    Assert.Contains("Det finns inga aktiva lån.", cut.Markup);
    Assert.Contains("Skapa nytt lån", cut.Markup);
  }

  private static LibraryContext CreateContext(string databaseName)
  {
    var options = new DbContextOptionsBuilder<LibraryContext>()
      .UseInMemoryDatabase(databaseName)
      .Options;

    return new LibraryContext(options);
  }
}
