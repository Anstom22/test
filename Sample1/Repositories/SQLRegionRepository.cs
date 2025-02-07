using Microsoft.EntityFrameworkCore;
using Sample1.Data;
using Sample1.Models.Domain;

namespace Sample1.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly AppDbContext dbContext;

        public SQLRegionRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(int id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;

        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(int id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateRegionAsync(int id, Region region)
        {
            var regions = await dbContext.Regions.FirstOrDefaultAsync(X => X.Id == id);
            if (regions == null)
            {
                return null;
            }
            
            regions.Name = region.Name;
            regions.Code= region.Code;
            await dbContext.SaveChangesAsync();
            return regions;

            
        }
    }
}
