using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly PicEnfermagemDb _dbContext;
    private readonly DbSet<Player> _players;

    public PlayerRepository(PicEnfermagemDb dbContext)
    {
        _dbContext = dbContext;
        _players = _dbContext.Set<Player>();
    }
    public async Task<bool> InsertPlayer(Player player)
    {
        await _players.AddAsync(player);

        var result = await _dbContext.SaveChangesAsync();

        if (result < 1)
            return false;

        return true;
    }
}
