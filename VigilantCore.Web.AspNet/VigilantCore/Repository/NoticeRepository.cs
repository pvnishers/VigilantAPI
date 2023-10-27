using Microsoft.EntityFrameworkCore;
using VigilantCore.Web.AspNet.Models;
using VigilantCore.Web.AspNet.Repository.Context;

namespace VigilantCore.Web.AspNet.Repository
{
    public class NoticeRepository : INoticeRepository
    {
        private readonly DataBaseContext _context;

        public NoticeRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<List<NoticeModel>> GetAllNoticesAsync(
            int? id = null,
            string? nationalities = null,
            string? nameForename = null)
        {
            var query = _context.Notices.AsQueryable();

            if (id.HasValue && id != 0)
            {
                query = query.Where(n => n.Id == id.Value);
            }

            if (!string.IsNullOrEmpty(nationalities))
            {
                var lowercaseNationalities = nationalities.ToLower();
                query = query.Where(n => n.Nationalities.ToLower().Contains(lowercaseNationalities));
            }

            if (!string.IsNullOrEmpty(nameForename))
            {
                var lowercaseNameForename = nameForename.ToLower();
                query = query.Where(n => (n.Name.ToLower() + " " + n.Forename.ToLower()).Contains(lowercaseNameForename));
            }

            return await query.ToListAsync();
        }

        public async Task AddRangeAsync(List<NoticeModel> notices)
        {
            await _context.Notices.AddRangeAsync(notices);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            _context.Notices.RemoveRange(_context.Notices);
            await _context.SaveChangesAsync();
        }
    }
}
