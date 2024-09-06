using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;

namespace PlayFieldBuddy.Api.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<bool> AddGame(GameCreateRequest game, Guid Id, CancellationToken cancellationToken)
        {
            var foundGame = await _gameRepository.GetGameById(Id, cancellationToken);
            if (foundGame != null)
            {
                return false;
            }

            var newGame = new Game
            {
                PlayersLimit = game.PlayersLimit,
                GameDate = game.GameDate,
                Pitch = game.Pitch
            };

            await _gameRepository.AddGame(newGame, cancellationToken);
            return true;
        }

        public async Task<bool> RemoveGame(Guid Id, CancellationToken cancellationToken)
        {
            var foundGame = await _gameRepository.GetGameById(Id, cancellationToken);
            if (foundGame == null)
            {
                return false;
            }

            await _gameRepository.DeleteGame(foundGame, cancellationToken);
            return true;
        }

        public async Task<bool> UpdateGame(Game game, Guid Id, CancellationToken cancellationToken)
        {
            var foundGame = await _gameRepository.GetGameById(Id, cancellationToken);
            if (foundGame == null)
            {
                return false;
            }

            foundGame.PlayersLimit = game.PlayersLimit;
            foundGame.GameDate = game.GameDate;
            foundGame.Pitch = game.Pitch;
            foundGame.Users = new List<User>();

            await _gameRepository.UpdateGame(foundGame, cancellationToken);
            return true;
        }
    }
}
