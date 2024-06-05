using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Implements
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;
        public SQLRegionRepository(NZWalksDbContext context) 
        {
            _context = context;
        }

        public async Task<Region> CreatedAsync(Region region)  
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task DeleteAsync(Guid id)
        {
            var region = await _context.Regions.SingleOrDefaultAsync(r => r.Id == id);
            if(region == null) { throw new KeyNotFoundException($"Entity with Id {id} not found."); }
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid regionId)
        {
            return await _context.Regions.SingleOrDefaultAsync(r => r.Id == regionId);
        }

        public async Task<Region> UpdatedAsync(Guid id, Region region)
        {
            var regionExisting = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (regionExisting != null)
            {
                regionExisting.Name = region.Name;
                regionExisting.Code = region.Code;
                regionExisting.RegionImageUrl = region.RegionImageUrl;

                await _context.SaveChangesAsync();
                return regionExisting;
            }

            return null;
        }
    }
}
