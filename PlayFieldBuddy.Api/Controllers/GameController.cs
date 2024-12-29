using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlayFieldBuddy.Api.Services;
using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace PlayFieldBuddy.Api.Controllers
{
    [ApiController]
    [Route("Games")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IGameRepository _gameRepository;
        private readonly ILogger _logger;

        public GameController(IGameService gameService, IGameRepository gameRepository, ILogger logger)
        {
            _gameService = gameService;
            _gameRepository = gameRepository;
            _logger = logger;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSingleGame([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var game = await _gameRepository.GetGameById(Id, cancellationToken);

                if (game == null)
                {
                    return NotFound("Game not found.");
                }

                // Mapowanie na DTO
                var gameDto = new GameDto
                {
                    Id = game.Id,
                    GameDate = game.GameDate,
                    PlayersLimit = game.PlayersLimit,
                    Users = game.Users.Select(u => new UserDto
                    {
                        Id = u.Id,
                        Username = u.Username
                    }).ToList()
                };

                return Ok(gameDto);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong while searching for Game. {ExceptionMessage}", ex.Message);
                return Problem();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames(CancellationToken cancellationToken)
        {
            try
            {
                var games = await _gameRepository.GetAllGames(cancellationToken);

                // Mapowanie na DTO
                var gamesDto = games.Select(g => new GameDto
                {
                    Id = g.Id,
                    GameDate = g.GameDate,
                    PlayersLimit = g.PlayersLimit,
                    Users = g.Users.Select(u => new UserDto
                    {
                        Id = u.Id,
                        Username = u.Username
                    }).ToList()
                }).ToList();

                return Ok(gamesDto);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong while searching for all Games. {ExceptionMessage}", ex.Message);
                return Problem();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] GameCreateRequest gameCreateRequest, CancellationToken cancellationToken)
        {
            try
            {
                await _gameService.AddGame(gameCreateRequest, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong while adding game. {ExceptionMessage}", ex.Message);
                return Problem();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGame([FromBody] GameUpdateRequest game, Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var updateGame = await _gameService.UpdateGame(game, id, cancellationToken);
                return updateGame ? Ok("Game updated successfully") : NotFound("Couldn't find the game");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong while updating game. {ExceptionMessage}", ex.Message);
                return Problem();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGame([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var deleteGame = await _gameService.RemoveGame(id, cancellationToken);
                return deleteGame ? Ok("Game deleted successfully") : NotFound("Couldn't find the game");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong while deleting the game. {ExceptionMessage}", ex.Message);
                return Problem();
            }
        }
    }
}
