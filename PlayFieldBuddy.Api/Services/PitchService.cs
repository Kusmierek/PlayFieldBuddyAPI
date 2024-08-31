using PlayFieldBuddy.Domain.Models.Pitch;
using PlayFieldBuddy.Repositories.Interfaces;

namespace PlayFieldBuddy.Api.Services
{
    public class PitchService : IPitchService
    {

        private readonly IPitchRepository _PitchRepository;

        public async Task<bool> AddPitch(PitchCreateRequest addPitch, CancellationToken cancellationToken)
        {
            var foundPitch = await _PitchRepository.GetByName(addPitch.Name, cancellationToken);
            if (foundPitch != null) { }

        }

        public Task<bool> DeletePitch(Guid Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePitch(Pitch pitch, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
