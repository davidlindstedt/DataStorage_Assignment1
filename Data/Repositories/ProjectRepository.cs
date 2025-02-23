using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
    {
        public ProjectRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProjectEntity>> GetAlllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<ProjectEntity?> GetAsync(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
