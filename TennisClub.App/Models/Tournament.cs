namespace TennisClub.App.Models;

public enum TournamentFormat
{
    League,
    Elimination
}

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TournamentFormat Format { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}
