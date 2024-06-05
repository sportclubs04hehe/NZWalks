using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Implements
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _context;

        public SQLWalkRepository(NZWalksDbContext context)
        {
            _context = context;
        }
        public async Task<Walk> Create(Walk walk)
        {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var walkExisting = await _context.Walks
                .FirstOrDefaultAsync(w => w.Id == id);

            if(walkExisting != null)
            {
                walkExisting.Name = walk.Name;
                walkExisting.Description = walk.Description;
                walkExisting.LengthInKm = walk.LengthInKm;
                walkExisting.WalkImageUrl = walk.WalkImageUrl;
                walkExisting.DifficultyId = walk.DifficultyId;
                walkExisting.RegionId = walk.RegionId;

                await _context.SaveChangesAsync();
                return walkExisting;
            }
            throw new KeyNotFoundException($"Entity with Id {id} not found.");
        }

        public async Task DeleteAsync(Guid id)
        {
            var walkExisting = await _context.Walks.FindAsync(id);

            if( walkExisting == null)
            {
                throw new KeyNotFoundException($"Entity with Id {id} not found.");
            }
            _context.Remove(walkExisting);
            await _context.SaveChangesAsync();
        }
    }
}
