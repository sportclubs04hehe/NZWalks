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

        public async Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscding = true,
            int pageNumer = 1,
            int pageSize = 1000)
        {
            // Get all records
            var walk = _context.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = walk.Where(w => w.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = isAscding ? walk.OrderBy(w => w.Name) 
                        : walk.OrderByDescending(w => w.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walk = isAscding ? walk.OrderBy(w => w.LengthInKm) 
                        : walk.OrderByDescending(w => w.LengthInKm);
                }
            }

            //Pagination
            var skipResults = (pageNumer -1) * pageSize;

            // return
            return await walk.Skip(skipResults).Take(pageSize).ToListAsync();
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
