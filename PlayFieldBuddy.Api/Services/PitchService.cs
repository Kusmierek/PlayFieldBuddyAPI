using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;

namespace PlayFieldBuddy.Api.Services
{
    public class PitchService : IPitchService
    {
        private readonly IPitchRepository _pitchRepository;

        public PitchService(IPitchRepository pitchRepository)
        {
            _pitchRepository = pitchRepository;
        }

        public async Task<bool> AddPitch(PitchCreateRequest addPitch, CancellationToken cancellationToken)
        {
            var foundPitch = await _pitchRepository.GetByName(addPitch.Name, cancellationToken);
            if (foundPitch != null)
            {
                return false;
            }

            var newPitch = new Pitch
            {
                Id = Guid.NewGuid(),
                Name = addPitch.Name,
                Address = addPitch.Address,
                Games = new List<Game>(),
                PitchType = PitchType.Uncovered
            };

            await _pitchRepository.AddPitch(newPitch, cancellationToken);
            return true;
        }

        public async Task<bool> DeletePitch(Guid Id, CancellationToken cancellationToken)
        {
            var foundPitch = await _pitchRepository.GetSinglePitchById(Id, cancellationToken);
            if (foundPitch == null)
            {
                return false;
            }

            await _pitchRepository.DeletePitch(foundPitch, cancellationToken);
            return true;
        }

        public async Task<bool> UpdatePitch(Pitch pitch, CancellationToken cancellationToken)
        {
            var foundPitch = await _pitchRepository.GetSinglePitchById(pitch.Id, cancellationToken);
            if (foundPitch == null)
            {
                return false;
            }

            foundPitch.Name = pitch.Name;
            foundPitch.Address = pitch.Address;
            foundPitch.Games = new List<Game>();

            await _pitchRepository.UpdatePitch(foundPitch, cancellationToken);
            return true;
        }
    }
}
