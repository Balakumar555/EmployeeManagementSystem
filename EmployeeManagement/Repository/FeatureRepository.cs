using EmployeeManagement.DB_Configuration;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmployeeManagement.Repository
{
    public class FeatureRepository : IFeature
    {
        private readonly ApplicationDBContext _dbContext;

        public FeatureRepository(ApplicationDBContext dbContext)
        {
        this._dbContext = dbContext;
        }

        public async Task<Feature> CreateUpdate(Feature feature)
        {
            if (feature != null)
            {
                if (feature.FeatureId == 0)
                {

                    await _dbContext.features.AddAsync(feature);
                    await _dbContext.SaveChangesAsync();
                    return feature;
                }
                else
                {
                    var existingfeature = await _dbContext.features.FindAsync(feature.FeatureId);
                    if (existingfeature != null)
                    {
                        existingfeature.Name = feature.Name;
                        existingfeature.Code = feature.Code;
                        existingfeature.IsActive = feature.IsActive;
                        existingfeature.ModifiedBy = feature.ModifiedBy;
                        existingfeature.ModifiedOn = DateTimeOffset.Now;
                        await _dbContext.SaveChangesAsync();
                        return existingfeature;
                    }
                }
            }
            return null;
        }

        public async Task<Feature> GetById(int id)
        {
            var feature = await _dbContext.features
                                   .FirstOrDefaultAsync(e => e.FeatureId == id);

            if (feature == null)
            {
                throw new KeyNotFoundException($"Feature with ID {id} not found.");
            }

            return feature;
        }

        public async Task<List<Feature>> GetFeatures()
        {
            var data = await _dbContext.features.ToListAsync();
            return data;
        }
    }
}
