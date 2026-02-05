using LibrarySystem.Core.Services;


namespace LibrarySystem.Core.Services
{
  public class Library
  {
    public BookCatalog Catalog { get; } = new();

  }
}