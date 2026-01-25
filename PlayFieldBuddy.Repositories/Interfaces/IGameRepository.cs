using PlayFieldBuddy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayFieldBuddy.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task AddGame (Game game, CancellationToken cancellationToken);

        Task<Game> GetGameById(Guid Id, CancellationToken cancellationToken);

        Task<IEnumerable<Game>> GetAllGames(CancellationToken cancellationToken);

        Task DeleteGame(Game game, CancellationToken cancellationToken);

        Task UpdateGame(Game game, CancellationToken cancellationToken);

    }
}
