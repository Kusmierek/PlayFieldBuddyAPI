using Microsoft.EntityFrameworkCore;
using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;

namespace PlayFieldBuddy.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly PlayFieldBuddyDbContext _dbContext;

        public GameRepository(PlayFieldBuddyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddGame(Game game, CancellationToken cancellationToken)
        {
            await _dbContext.Games.AddAsync(game, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteGame(Game game, CancellationToken cancellationToken)
        {
            _dbContext.Games.Remove(game);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Game>> GetAllGames(CancellationToken cancellationToken)
        {
            return await _dbContext.Games.Include(p=>p.Pitch)
                .Include(p => p.Owner)
                .Include(p => p.Users)
                .ToListAsync(cancellationToken);
        }

        public async Task<Game> GetGameById(Guid Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Games
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task UpdateGame(Game game, CancellationToken cancellationToken)
        {
            _dbContext.Games.Update(game);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
