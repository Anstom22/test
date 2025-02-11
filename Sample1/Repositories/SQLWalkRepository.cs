using Sample1.Data;
using Sample1.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Sample1.Repositories
{
    public class SQLWalkRepository:IWalkRepository
    {
        private readonly AppDbContext dbContext;

        public SQLWalkRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteWalkAsync(int id)
        {
            var walk= await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(int id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            
            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(int id, Walk walk)
        {
            var existingWalks= await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(y => y.Id == id);
            if(existingWalks == null)
            {
                return null;
            }
            else
            {
                existingWalks.Description = walk.Description;
                existingWalks.Name = walk.Name;
                existingWalks.LengthInKm = walk.LengthInKm;
                existingWalks.WalkImageUrl = walk.WalkImageUrl;
                existingWalks.RegionId = walk.RegionId;
                existingWalks.DifficultyId = walk.DifficultyId;
                existingWalks.Difficulty = walk.Difficulty;
                existingWalks.Region = walk.Region;
                await dbContext.SaveChangesAsync();
            }
            
            
            
            return existingWalks;

        }
    }
}
