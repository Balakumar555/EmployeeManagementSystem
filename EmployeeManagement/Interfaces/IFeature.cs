using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IFeature
    {
        Task<Feature> CreateUpdate(Feature feature);
        Task<Feature> GetById(int id);
        Task <List<Feature>> GetFeatures();
    }
}
