using Microsoft.EntityFrameworkCore;
using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayFieldBuddy.Repositories
{
    public class PitchRepository : IPitchRepository
    {
        private readonly PlayFieldBuddyDbContext _context;

        public PitchRepository(PlayFieldBuddyDbContext context)
        {
            _context = context;
        }

        public async Task AddPitch(Pitch pitch, CancellationToken cancellationToken)
        {
            await _context.Pitches.AddAsync(pitch, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePitch(Pitch pitch, CancellationToken cancellationToken)
        {
            _context.Pitches.Remove(pitch);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Pitch>> GetAllPitches(CancellationToken cancellationToken)
        {
            return await _context.Pitches
                .ToListAsync(cancellationToken);
        }

        public async Task<Pitch> GetByName(string name, CancellationToken cancellationToken)
        {
            return await _context.Pitches
                .FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
        }

        public async Task<Pitch> GetSinglePitchById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Pitches                
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task UpdatePitch(Pitch pitch, CancellationToken cancellationToken)
        {
            _context.Pitches.Update(pitch);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
