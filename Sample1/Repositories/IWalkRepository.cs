using Sample1.Models.Domain;

namespace Sample1.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetWalkByIdAsync(int id);
        Task<Walk?> UpdateWalkAsync(int id, Walk walk);
        Task<Walk?> DeleteWalkAsync(int id);
    }
}
