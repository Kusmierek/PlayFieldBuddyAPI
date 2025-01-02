using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;

namespace PlayFieldBuddy.Api.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPitchRepository _pitchRepository;
        private readonly IUserRepository _userRepository;


        public GameService(IGameRepository gameRepository, IPitchRepository pitchRepository, IUserRepository userRepository)
        {
            _gameRepository = gameRepository;
            _pitchRepository = pitchRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddGame(GameCreateRequest game, CancellationToken cancellationToken)
        {
            var foundPitch = await _pitchRepository.GetSinglePitchById(game.PitchId, cancellationToken);
            var foundUser = await _userRepository.GetSingleUserById(game.OwnerId, cancellationToken);
            
            if (foundPitch is null || foundUser is null)
            {
                return false;
            }
            
            var newGame = new Game
            {
                Id = Guid.NewGuid(),
                PlayersLimit = game.PlayersLimit,
                GameDate = game.GameDate,
                Pitch = foundPitch,
                Owner = foundUser
            };

            var user = await _userRepository.GetSingleUserById(game.UserId, cancellationToken);
            if (user != null && !newGame.Users.Contains(user))
            {
                newGame.Users.Add(user);
            }

            await _gameRepository.AddGame(newGame, cancellationToken);
            return true;
        }

        public async Task<bool> RemoveGame(Guid id, CancellationToken cancellationToken)
        {
            var foundGame = await _gameRepository.GetGameById(id, cancellationToken);
            if (foundGame == null)
            {
                return false;
            }

            await _gameRepository.DeleteGame(foundGame, cancellationToken);
            return true;
        }

        public async Task<bool> UpdateGame( GameUpdateRequest game, Guid Id, CancellationToken cancellationToken)
        {
            var foundGame = await _gameRepository.GetGameById(Id, cancellationToken);
            if (foundGame == null)
            {
                return false;
            }

            foundGame.PlayersLimit = game.PlayersLimit;
            foundGame.GameDate = game.GameDate;
            
            var user = await _userRepository.GetSingleUserById(game.UserId, cancellationToken);
            if (user != null && !foundGame.Users.Contains(user))
            {
                foundGame.Users.Add(user);
            }

            await _gameRepository.UpdateGame(foundGame, cancellationToken);
            return true;
        }
    }
}
