using VigilantCore.Web.AspNet.Models;

namespace VigilantCore.Web.AspNet.Repositories
{
    public interface IWantedRepository
    {
        Task AddRangeAsync(List<WantedModel> wantedList);
        Task<List<WantedModel>> GetAllWantedAsync(
            int? id = null,
            string title = null,
            string subject = null,
            string nationality = null,
            string sex = null,
            string race = null);
        Task DeleteAllAsync();
    }

}
