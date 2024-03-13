using GameStore.Api.Data;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameStore.Api.Repositories;

public class EntityFrameworkGamesRepository : IGamesRepository
{
    private readonly GameStoreContext dbContext;
    private readonly ILogger<EntityFrameworkGamesRepository> logger;

    public EntityFrameworkGamesRepository(GameStoreContext dbContext, ILogger<EntityFrameworkGamesRepository> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task CreateAsync(Game game)
    {
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("New game created with name {Name} having price {Price}",game.Name,game.Price);
        
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.Games.Where(g => g.Id == id)
                        .ExecuteDeleteAsync();
    }

    public async Task<Game?> GetAsync(int id)
    {
        return  await dbContext.Games.FindAsync(id);
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await dbContext.Games.AsNoTracking().ToListAsync();
    }

    public async Task UpdateAsync(Game updatedGame)
    {
        dbContext.Games.Update(updatedGame);
        await dbContext.SaveChangesAsync();
    }
}