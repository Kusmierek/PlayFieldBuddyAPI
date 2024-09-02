using PlayFieldBuddy.Domain.Models;

using PlayFieldBuddy.Repositories.Interfaces;

namespace PlayFieldBuddy.Api.Services
{
    public class PitchService : IPitchService
    {

        private readonly IPitchRepository _PitchRepository;

        public PitchService(IPitchRepository pitchRepository)
        {
            _PitchRepository = pitchRepository;
        }


        public async Task<bool> AddPitch(PitchCreateRequest addPitch, CancellationToken cancellationToken)
        {
            var foundPitch = await _PitchRepository.GetByName(addPitch.Name, cancellationToken);
            if (foundPitch != null)
            {
                return false;

            
            }
            var newPitch = new Pitch()
            {

                Id = new Guid(),
                Name = addPitch.Name,
                adress = addPitch.adress,
                Games = new List<Game>(),
                PitchType = PitchType.Uncovered


            };

           await _PitchRepository.AddPitch(newPitch, cancellationToken);
            return true;


        }

        public async Task<bool> DeletePitch(Guid Id, CancellationToken cancellationToken)
        {
            var foundPitch = await _PitchRepository.GetSinglePitchById(Id, cancellationToken);
            if (foundPitch == null)
            {
                return false;


            }

            await _PitchRepository.DeletePitch(foundPitch, cancellationToken);
            return true;
        }

        public async Task<bool> UpdatePitch(Pitch pitch, CancellationToken cancellationToken)
        {
            var foundPitch = await _PitchRepository.GetByName(pitch.Name, cancellationToken);
            if (foundPitch != null)
            {
                return false;


            }

           foundPitch.Games = new List<Game>();



            await _PitchRepository.UpdatePitch(foundPitch, cancellationToken);
            return true;

        }
    }
}
