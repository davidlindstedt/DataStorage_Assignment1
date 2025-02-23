using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task CreateProjectAsync(ProjectEntity project)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectEntity>> GetAllOrdersAsync()
        {
            return await _projectRepository.GetAlllAsync();
        }

        public async Task<ProjectEntity?> GetByIdAsync(int id)
        {
            return await _projectRepository.GetAsync(id);
        }


        public async Task<bool> UpdateProjectAsync(ProjectEntity project)
        {
            if (project == null || project.Id <= 0)
                return false;

            var existingProject = await _projectRepository.GetAsync(project.Id);
            if (existingProject == null)
                return false;

            existingProject.Title = project.Title;
            existingProject.Description = project.Description;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.CustomerId = project.CustomerId;
            existingProject.StatusId = project.StatusId;
            existingProject.UserId = project.UserId;
            existingProject.ProductId = project.ProductId;

            try
            {
                if (_projectRepository is BaseRepository<ProjectEntity> baseRepo)
                {
                    await baseRepo.UpdateAsync(existingProject);
                }
                else
                {
                    throw new InvalidOperationException("UpdateAsync inte implementerat i repositoryt.");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
