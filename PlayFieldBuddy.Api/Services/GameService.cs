using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;

namespace PlayFieldBuddy.Api.Services
{
    public class GameService : IGameService
    {

        private readonly IGameRepository _gameRepository;

        GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
            
        }

        public async Task<bool> AddGame(GameCreateRequest game,Guid Id, CancellationToken cancellationToken)
        {
            var foundGame = await _gameRepository.GetGameById(Id, cancellationToken);
            if (foundGame != null)
            {
                return false;
            }

            var newGame = new Game();
            {
                newGame.PlayersLimit = game.PlayersLimit;
                newGame.GameDate = game.GameDate;
                newGame.Pitch = game.Pitch;

            }

            return true;



        }

        public async Task<bool> RemoveGame( Guid Id, CancellationToken cancellationToken)
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
