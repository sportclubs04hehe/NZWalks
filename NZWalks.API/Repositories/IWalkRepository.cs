using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> Create(Walk walk);
        Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscding = true,
            int pageNumer = 1,
            int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task DeleteAsync(Guid id);
    }
}
