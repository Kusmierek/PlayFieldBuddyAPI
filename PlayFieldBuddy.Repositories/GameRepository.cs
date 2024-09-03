using Microsoft.EntityFrameworkCore;
using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayFieldBuddy.Repositories
{
    public class GameRepository : IGameRepository
    {

        PlayFieldBuddyDbContext _dbContext;

        GameRepository(PlayFieldBuddyDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task AddGame(Game game, CancellationToken cancellationToken)
        {
            await _dbContext.Games.AddAsync(game, cancellationToken);

            await _dbContext.SaveChangesAsync();
          


        }

        public async Task DeleteGame(Game game, CancellationToken cancellationToken)
        {
             _dbContext.Games.Remove(game);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Game>> GetAllGames(CancellationToken cancellationToken)
        {
           return await _dbContext.Games
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
