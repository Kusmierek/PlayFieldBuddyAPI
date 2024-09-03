using Microsoft.AspNetCore.Mvc;
using PlayFieldBuddy.Api.Services;
using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;
using ILogger = Serilog.ILogger;


namespace PlayFieldBuddy.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;



    public UserController(IUserService userService, IUserRepository userRepository, ILogger logger)
    {
        _userService = userService;
        _userRepository = userRepository;
        _logger = logger;
    }
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingleUser(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetSingleUserById(id, cancellationToken);
            
            return user is null ? NotFound() : Ok(user);
        }
        catch (Exception exception)
        {
            _logger.Error(exception,
                "Something went wrong while deleting event. {ExceptionMessage}",
                exception.Message);

            return Problem();
        }
    }
    
    
    [HttpPost]
    public async Task<ActionResult> AddUser([FromBody] UserCreateRequest user, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.AddUser(user, cancellationToken);
            
            return Ok("User created successfully");
        }
        catch (Exception exception)
        {
            _logger.Error(exception,
                "Something went wrong while deleting event. {ExceptionMessage}",
                exception.Message);

            return Problem();
        }
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        try
        {
            var usersList = await _userRepository.GetManyUsers(cancellationToken);
            
            return Ok(usersList);
        }
        catch (Exception exception)
        {
            _logger.Error(exception,
                "Something went wrong while deleting event. {ExceptionMessage}",
                exception.Message);

            return Problem();
        }
    }
    
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser(User user, CancellationToken cancellationToken)
    {
        try
        {
            var updatedUser = await _userService.UpdateUser(user, cancellationToken);
            
            return updatedUser is false ? NotFound("Couldn't find the user") : Ok("User updated successfully");
        }
        catch (Exception exception)
        {
            _logger.Error(exception,
                "Something went wrong while updating event. {ExceptionMessage}",
                exception.Message);
    
            return Problem();
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var deletedEvent = await _userService.DeleteUser(id, cancellationToken);
            
            return deletedEvent is false ? NotFound("Couldn't find the user") : Ok("Usere deleted successfully");
        }
        catch (Exception exception)
        {
            _logger.Error(exception,
                "Something went wrong while deleting event. {ExceptionMessage}",
                exception.Message);
    
            return Problem();
        }
    }
    
}