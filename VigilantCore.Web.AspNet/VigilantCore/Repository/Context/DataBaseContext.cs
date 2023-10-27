using Microsoft.EntityFrameworkCore;
using VigilantCore.Web.AspNet.Models;

namespace VigilantCore.Web.AspNet.Repository.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<NoticeModel> Notices { get; set; }
        public DbSet<WantedModel> Wanteds { get; set; }

    }

}
