using Microsoft.EntityFrameworkCore;
using TennisClub.App.Data;
using TennisClub.App.Models;

namespace TennisClub.App.Services;

public class MatchService
{
    private readonly TennisDbContext _db;
    private readonly PlayerService _playerService;

    public MatchService(TennisDbContext db, PlayerService playerService)
    {
        _db = db;
        _playerService = playerService;
    }

    public async Task<List<Match>> GetAllMatchesAsync()
        => await _db.Matches
            .Include(m => m.Player1)
            .Include(m => m.Player2)
            .Include(m => m.Winner)
            .Include(m => m.Tournament)
            .OrderByDescending(m => m.PlayedAt)
            .ToListAsync();

    public async Task<List<Match>> GetRecentMatchesAsync(int count = 5)
        => await _db.Matches
            .Include(m => m.Player1)
            .Include(m => m.Player2)
            .Include(m => m.Winner)
            .OrderByDescending(m => m.PlayedAt)
            .Take(count)
            .ToListAsync();

    public async Task<List<Match>> GetMatchesByPlayerAsync(int playerId)
        => await _db.Matches
            .Include(m => m.Player1)
            .Include(m => m.Player2)
            .Include(m => m.Winner)
            .Include(m => m.Tournament)
            .Where(m => m.Player1Id == playerId || m.Player2Id == playerId)
            .OrderByDescending(m => m.PlayedAt)
            .ToListAsync();

    public async Task CreateMatchAsync(Match match)
    {
        _db.Matches.Add(match);
        await _db.SaveChangesAsync();

        // Award points: +10 for win, +3 for loss
        await _playerService.AddPointsAsync(match.WinnerId, 10);
        int loserId = match.WinnerId == match.Player1Id ? match.Player2Id : match.Player1Id;
        await _playerService.AddPointsAsync(loserId, 3);
    }

    public async Task<List<Tournament>> GetAllTournamentsAsync()
        => await _db.Tournaments.OrderBy(t => t.Name).ToListAsync();

    public async Task<List<StandingEntry>> GetStandingsAsync()
    {
        var players = await _db.Players.ToListAsync();
        var matches = await _db.Matches.ToListAsync();

        return players.Select(p =>
        {
            var wins = matches.Count(m => m.WinnerId == p.Id);
            var losses = matches.Count(m => (m.Player1Id == p.Id || m.Player2Id == p.Id) && m.WinnerId != p.Id);
            return new StandingEntry
            {
                Player = p,
                Wins = wins,
                Losses = losses
            };
        })
        .OrderByDescending(s => s.Player.RankingPoints)
        .ToList();
    }
}

public class StandingEntry
{
    public Player Player { get; set; } = null!;
    public int Wins { get; set; }
    public int Losses { get; set; }
}
