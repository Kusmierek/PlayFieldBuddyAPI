using Microsoft.AspNetCore.Mvc;
using PlayFieldBuddy.Api.Services;
using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace PlayFieldBuddy.Api.Controllers
{

    [ApiController]
    [Route("pitches")]

    public class PitchController : ControllerBase
    {

        private readonly IPitchService _pitchService;
        private readonly IPitchRepository _pitchRepository;
        private readonly ILogger _logger;

        public PitchController(IPitchService pitchService, IPitchRepository pitchRepository, ILogger logger)
        {
            _pitchService = pitchService;
            _pitchRepository = pitchRepository;
            _logger = logger;
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> GetSinglePitch ([FromRoute]Guid Id, CancellationToken cancellationToken)
        {
            var pitch = await _pitchRepository.GetSinglePitchById(Id, cancellationToken);

            
            if (pitch == null)
            {
                return NotFound();
            }

           
            return Ok(pitch);

        }

        [HttpGet]

        public async Task<IActionResult> GetAllPitches (CancellationToken cancellationToken)
        {
            var pitches = await _pitchRepository.GetAllPitches(cancellationToken);

            if (pitches == null)
            {
                return Problem();
            }

            return Ok(pitches);

        }


        [HttpPost]
        public async Task<IActionResult> AddPitch ([FromBody]PitchCreateRequest pitchCreateRequest, CancellationToken cancellationToken)
        {
            try
            {
                await _pitchService.AddPitch(pitchCreateRequest, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong while adding pitch. {ExceptionMessage}", ex.Message);
                return Problem();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePitch(Pitch pitch, CancellationToken cancellationToken)
        {
            try
            {
                var updatePitch = await _pitchService.UpdatePitch(pitch, cancellationToken);
                return updatePitch ? Ok("Pitch updated successfully") : NotFound("Couldn't find the pitch");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong while updating pitch. {ExceptionMessage}", ex.Message);
                return Problem();
            }



        }


        [HttpDelete("{Id}")]

        public async Task<IActionResult>DeletePitch([FromRoute]Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var deletePitch = await _pitchService.DeletePitch(Id, cancellationToken);
                return deletePitch? Ok("Pitch deleted successfully") : NotFound("Couldn't find the pitch");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong while updating pitch. {ExceptionMessage}", ex.Message);
                return Problem();
            }

        }

    }


}

