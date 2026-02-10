using System.Text.Json;
using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;

public static class LibrarySeeder
{
  public static void SeedFromJson(Library library)
  {
    SeedMembers(library);
    SeedBooks(library);
  }

  // 📚 BOOKS
  private static void SeedBooks(Library library)
  {
    var path = Path.Combine(AppContext.BaseDirectory, "Data", "books.json");
    if (!File.Exists(path)) return;

    var json = File.ReadAllText(path);
    var items = JsonSerializer.Deserialize<List<BookDto>>(json, JsonOptions);

    if (items is null) return;

    foreach (var i in items)
      library.Catalog.Add(new Book(i.Isbn, i.Title, i.Author, i.PublishedYear));
  }

  // 👤 MEMBERS
  private static void SeedMembers(Library library)
  {
    var path = Path.Combine(AppContext.BaseDirectory, "Data", "members.json");
    if (!File.Exists(path)) return;

    var json = File.ReadAllText(path);
    var items = JsonSerializer.Deserialize<List<MemberDto>>(json, JsonOptions);

    if (items is null) return;

    foreach (var m in items)
      library.Members.Add(new Member(m.MemberId, m.Name, m.Email));
  }

  private static readonly JsonSerializerOptions JsonOptions = new()
  {
    PropertyNameCaseInsensitive = true
  };

  private sealed class BookDto
  {
    public string Isbn { get; set; } = "";
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int PublishedYear { get; set; }
  }

  private sealed class MemberDto
  {
    public string MemberId { get; set; } = "";
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
  }
}
