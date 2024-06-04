using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid regionId);
        Task<Region> CreatedAsync(Region region);
        Task<Region> UpdatedAsync(Guid id,Region region);
        Task DeleteAsync(Guid id);
    }
}
