using PlayFieldBuddy.Domain.Models;

namespace PlayFieldBuddy.Api.Services
{
    public interface IGameService
    {
        Task<bool> AddGame(GameCreateRequest game, CancellationToken cancellationToken);

        Task<bool> RemoveGame(Guid id, CancellationToken cancellationToken);

        Task<bool> UpdateGame(Game game, Guid id, CancellationToken cancellationToken);  


    }
}
