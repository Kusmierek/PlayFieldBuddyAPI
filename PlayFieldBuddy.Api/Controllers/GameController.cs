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
        public async Task<ActionResult> GetSingleGame([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var getGame = await _gameRepository.GetGameById(Id, cancellationToken);

                return Ok(getGame);

               
            }
            catch (Exception ex)
            {

                _logger.Error(ex, "Something went wrong while searching for Game. {ExceptionMessage}", ex.Message);
                return Problem();
            }
            



        }


        [HttpGet]

        public async Task<IActionResult> GetAllGames( CancellationToken cancellationToken)
        {
            try
            {
                var getGames = await _gameRepository.GetAllGames(cancellationToken);

                return Ok(getGames);


            }
            catch (Exception ex)
            {

                _logger.Error(ex, "Something went wrong while searching for all Games. {ExceptionMessage}", ex.Message);
                return Problem();
            }




        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> AddGame([FromBody] GameCreateRequest gameCreateRequest,[FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                await _gameService.AddGame(gameCreateRequest,Id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong while adding game. {ExceptionMessage}", ex.Message);
                return Problem();
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateGame([FromRoute] Guid Id, [FromBody] Game game, CancellationToken cancellationToken)
        {
            try
            {
                var updateGame = await _gameService.UpdateGame(game, Id, cancellationToken);
                return updateGame ? Ok("Game updated successfully") : NotFound("Couldn't find the game");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong while updating game. {ExceptionMessage}", ex.Message);
                return Problem();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGame([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var deleteGame = await _gameService.RemoveGame(Id, cancellationToken);
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
