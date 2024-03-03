using Microsoft.EntityFrameworkCore;
using Wms.Infrastructure.Models;

namespace Wms.Infrastructure.Services
{
    public interface ISampleService
    {
        Task<List<Sample>> GetAllSamples();
        Task<Sample?> GetSampleById(int id);
        Task CreateOrUpdateSample(Sample sample);
        Task DeleteSample(int id);
    }

    public class SampleService : ISampleService
    {
        private readonly WmsDbContext _dbContext;

        public SampleService(WmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Sample>> GetAllSamples()
        {
            return await _dbContext.Samples.ToListAsync();
        }

        public async Task<Sample?> GetSampleById(int id)
        {
            return await _dbContext.Samples.FindAsync(id);
        }

        public async Task CreateOrUpdateSample(Sample sample)
        {
            if (_dbContext.Samples.Find(sample.Id) == null)
            {
                await _dbContext.Samples.AddAsync(sample);
            }
            else
            {
                _dbContext.Entry(sample).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSample(int id)
        {
            var sample = await _dbContext.Samples.FindAsync(id);
            if (sample != null)
            {
                _dbContext.Samples.Remove(sample);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
