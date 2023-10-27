using Microsoft.EntityFrameworkCore;
using VigilantCore.Web.AspNet.Models;
using VigilantCore.Web.AspNet.Repositories;
using VigilantCore.Web.AspNet.Repository.Context;

namespace VigilantCore.Web.AspNet.Repository
{
    public class WantedRepository : IWantedRepository
    {
        private readonly DataBaseContext _dbContext;

        public WantedRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRangeAsync(List<WantedModel> wantedList)
        {
            await _dbContext.Wanteds.AddRangeAsync(wantedList);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAllAsync()
        {
            _dbContext.Wanteds.RemoveRange(_dbContext.Wanteds);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<WantedModel>> GetAllWantedAsync(
            int? id = null,
            string title = null,
            string subject = null,
            string nationality = null,
            string sex = null,
            string race = null)
        {
            var query = _dbContext.Wanteds.AsQueryable();

            if (id.HasValue && id != 0)
            {
                query = query.Where(w => w.Id.Equals(id.Value));
            }

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(w => w.Title.ToLower().Contains(title.ToLower()));
            }

            if (!string.IsNullOrEmpty(subject))
            {
                query = query.Where(w => w.Subjects.ToLower().Contains(subject.ToLower()));
            }

            if (!string.IsNullOrEmpty(nationality))
            {
                query = query.Where(w => w.Nationality.ToLower() == nationality.ToLower());
            }

            if (!string.IsNullOrEmpty(sex))
            {
                query = query.Where(w => w.Sex.ToLower() == sex.ToLower());
            }

            if (!string.IsNullOrEmpty(race))
            {
                query = query.Where(w => w.Race.ToLower() == race.ToLower());
            }

            return await query.ToListAsync();
        }


    }
}
