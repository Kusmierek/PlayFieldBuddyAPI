using PlayFieldBuddy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayFieldBuddy.Repositories.Interfaces
{
    public interface IPitchRepository
    {
        Task<Pitch> GetSinglePitchById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Pitch>> GetAllPitches(CancellationToken cancellationToken);
        Task<Pitch> GetByName(string name, CancellationToken cancellationToken);
        Task AddPitch(Pitch pitch, CancellationToken cancellationToken);
        Task UpdatePitch(Pitch pitch, CancellationToken cancellationToken);
        Task DeletePitch(Pitch pitch, CancellationToken cancellationToken);
    }
}
