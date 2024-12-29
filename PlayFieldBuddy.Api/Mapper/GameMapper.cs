using AutoMapper;
using PlayFieldBuddy.Domain.Models;

public class GameMapper : Profile
{
    public GameMapper()
    {
        CreateMap<Game, GameDto>();
          

        CreateMap<User, UserDto>();


        CreateMap<Pitch, PitchDto>();
    }
}