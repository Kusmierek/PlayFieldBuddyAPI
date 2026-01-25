using PlayFieldBuddy.Domain.Models;




namespace PlayFieldBuddy.Api.Services
{
    public interface IPitchService
    {
        public Task<bool> AddPitch(PitchCreateRequest addPitch, CancellationToken cancellationToken);

        public Task<bool> UpdatePitch(Pitch pitch, CancellationToken cancellationToken);

        public Task<bool> DeletePitch(Guid Id, CancellationToken cancellationToken);


    }
}
