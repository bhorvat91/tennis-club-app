using Microsoft.EntityFrameworkCore;
using TennisClub.App.Data;
using TennisClub.App.Models;

namespace TennisClub.App.Services;

public class PlayerService
{
    private readonly TennisDbContext _db;

    public PlayerService(TennisDbContext db)
    {
        _db = db;
    }

    public async Task<List<Player>> GetAllPlayersAsync()
        => await _db.Players.OrderByDescending(p => p.RankingPoints).ToListAsync();

    public async Task<Player?> GetPlayerByIdAsync(int id)
        => await _db.Players.FindAsync(id);

    public async Task<List<Player>> GetTopPlayersAsync(int count = 5)
        => await _db.Players.OrderByDescending(p => p.RankingPoints).Take(count).ToListAsync();

    public async Task CreatePlayerAsync(Player player)
    {
        player.CreatedAt = DateTime.UtcNow;
        _db.Players.Add(player);
        await _db.SaveChangesAsync();
    }

    public async Task UpdatePlayerAsync(Player player)
    {
        _db.Players.Update(player);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> DeletePlayerAsync(int id)
    {
        var player = await _db.Players.FindAsync(id);
        if (player == null) return false;

        bool hasMatches = await _db.Matches
            .AnyAsync(m => m.Player1Id == id || m.Player2Id == id || m.WinnerId == id);
        if (hasMatches) return false;

        _db.Players.Remove(player);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task AddPointsAsync(int playerId, int points)
    {
        var player = await _db.Players.FindAsync(playerId);
        if (player != null)
        {
            player.RankingPoints += points;
            await _db.SaveChangesAsync();
        }
    }
}
