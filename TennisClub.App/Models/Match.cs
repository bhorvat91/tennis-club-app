namespace TennisClub.App.Models;

public class Match
{
    public int Id { get; set; }
    public int Player1Id { get; set; }
    public int Player2Id { get; set; }
    public int? TournamentId { get; set; }
    public string Score { get; set; } = string.Empty;
    public int WinnerId { get; set; }
    public DateTime PlayedAt { get; set; }
    public Player Player1 { get; set; } = null!;
    public Player Player2 { get; set; } = null!;
    public Player Winner { get; set; } = null!;
    public Tournament? Tournament { get; set; }
}
