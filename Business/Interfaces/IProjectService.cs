using Data.Entities;
namespace Business.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectEntity>> GetAllOrdersAsync();
    Task<ProjectEntity> GetByIdAsync(int id);
}
