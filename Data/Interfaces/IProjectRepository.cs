using Data.Entities;

namespace Data.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectEntity>> GetAlllAsync();
        Task<ProjectEntity?> GetAsync(int id);
        Task UpdateAsync(ProjectEntity entity); 
    }
}
