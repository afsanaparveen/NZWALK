using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.models.domains;

namespace NzWalks.API.repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        public readonly NzWalksDbContext dbContext;
        public SQLRegionRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

       
        public NzWalksDbContext DbContext { get; }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();

        }
        public async Task<Region?> GetById(Guid id)
        {

            return await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

        }
        public async Task<Region?> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;

        }
        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (existingRegion == null)
            {
                return null;

            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageURL = region.RegionImageURL;
            await dbContext.SaveChangesAsync();
            return existingRegion;



        }


        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public Task<Region> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
