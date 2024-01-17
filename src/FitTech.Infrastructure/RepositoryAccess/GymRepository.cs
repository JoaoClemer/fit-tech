using FitTech.Domain.Entities;
using FitTech.Domain.Repositories.Gym;
using FitTech.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FitTech.Infrastructure.RepositoryAccess
{
    public class GymRepository : IGymReadOnlyRepository, IGymWriteOnlyRepository
    {
        private readonly FitTechContext _context;

        public GymRepository(FitTechContext context)
        {
            _context = context;
        }

        public async Task CreateGym(Gym gym)
        {
            await _context.Gyms.AddAsync(gym);
        }

        public async Task<Gym?> GetGymByEmail(string email)
        {
            return await _context.Gyms.
                AsNoTracking().
                FirstOrDefaultAsync(g => g.EmailAddress.Equals(email));
        }

        public async Task<Gym?> GetGymById(int id)
        {
            return await _context.Gyms.FirstOrDefaultAsync(g => g.Id.Equals(id));
        }

        public async Task<Gym?> GetGymByName(string name)
        {
            return await _context.Gyms.FirstOrDefaultAsync(g => g.Name.Equals(name));
        }
    }
}
