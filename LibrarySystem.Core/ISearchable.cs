namespace LibrarySystem.Core.Abstractions;
public interface ISearchable
{
    bool Matches(string searchTerm);
}
