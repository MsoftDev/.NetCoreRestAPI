using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> games = new()
    {
        new Game()
        {
            Id = 1,
            Name = "Mario Bros.",
            Genre = "Genre1",
            Price = 19.99M,
            ImageUri = "https://placeholder.co/100",
            ReleaseDate = new DateTime(1998,04,01)
        },
        new Game()
        {
            Id = 2,
            Name = "Candy Cane",
            Genre = "Genre2",
            Price = 59.99M,
            ImageUri = "https://placeholder.co/100",
            ReleaseDate = new DateTime(2013,06,13)
        }
    };

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await Task.FromResult(games);
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await Task.FromResult(games.Find(g => g.Id == id));
    }

    public async Task CreateAsync(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Game game)
    {
        int index = games.FindIndex(g => g.Id == game.Id);
        games[index] = game;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        int index = games.FindIndex(g => g.Id == id);
        games.RemoveAt(index);
        await Task.CompletedTask;
    }
}