using Sample1.Models.Domain;
using System.Runtime.InteropServices;

namespace Sample1.Repositories
{
    public interface IRegionRepository
    {
       Task<List<Region>> GetAllAsync();
       Task<Region?> GetByIdAsync(int id);
       Task<Region>CreateAsync(Region region);
       Task<Region?>UpdateRegionAsync(int id, Region region);
       Task<Region?> DeleteRegionAsync(int id);
    }
}
