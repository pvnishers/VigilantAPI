using VigilantCore.Web.AspNet.Models;

namespace VigilantCore.Web.AspNet.Repository
{
    public interface INoticeRepository
    {
        Task<List<NoticeModel>> GetAllNoticesAsync(
            int? id = null,
            string? nationalities = null,
            string? nameForename = null);

        Task AddRangeAsync(List<NoticeModel> notices);
        Task DeleteAllAsync();
    }
}
