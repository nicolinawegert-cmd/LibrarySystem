namespace LibrarySystem.Core;
public interface ISearchable
{
    bool MatchesQuery(string SearchTerm);
}
