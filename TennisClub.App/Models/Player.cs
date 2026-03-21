namespace TennisClub.App.Models;

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int RankingPoints { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Match> MatchesAsPlayer1 { get; set; } = new List<Match>();
    public ICollection<Match> MatchesAsPlayer2 { get; set; } = new List<Match>();
}
