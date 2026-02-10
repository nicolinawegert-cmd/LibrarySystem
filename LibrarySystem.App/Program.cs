using LibrarySystem.Core.Services;

var library = new Library();

LibrarySeeder.SeedFromJson(library);

var menu = new ConsoleMenu(library);
menu.Run();