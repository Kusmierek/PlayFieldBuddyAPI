using PlayFieldBuddy.Domain.Models;

namespace PlayFieldBuddy.Api.Services
{
    public interface IGameService
    {
        Task<bool> AddGame(GameCreateRequest game,Guid Id, CancellationToken cancellationToken);

        Task<bool> RemoveGame(Guid Id, CancellationToken cancellationToken);

        Task<bool> UpdateGame(Game game, Guid Id, CancellationToken cancellationToken);  


    }
}
