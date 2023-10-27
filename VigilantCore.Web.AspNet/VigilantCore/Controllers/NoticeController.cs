using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PagedList;
using VigilantCore.Web.AspNet.DTOs;
using VigilantCore.Web.AspNet.Models;
using VigilantCore.Web.AspNet.Repository;

namespace VigilantCore.Web.AspNet.Controllers
{
    [ApiController]
    [Route("interpol")]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeRepository _repository;

        public NoticesController(INoticeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetNotices")]
        public async Task<IActionResult> GetNotices()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync("https://ws-public.interpol.int/notices/v1/red?resultPerPage=160&page=1");
                var rootObject = JsonConvert.DeserializeObject<RootObject>(response);
                var notices = rootObject._embedded.notices;

                var newNotices = new List<NoticeModel>();

                foreach (var notice in notices)
                {
                    var newNotice = new NoticeModel(
                        notice.date_of_birth ?? string.Empty,
                        notice.nationalities ?? new List<string>(),
                        notice.entity_id ?? string.Empty,
                        notice.forename ?? string.Empty,
                        notice.name ?? string.Empty,
                        notice._links?.thumbnail?.href ?? string.Empty
                    );

                    newNotices.Add(newNotice);
                }

                await _repository.DeleteAllAsync();

                await _repository.AddRangeAsync(newNotices);
            }

            return Ok("Dados salvos com sucesso");
        }

        [HttpGet("GetAllNotices")]
        public async Task<IActionResult> GetAllNotices(int? page, int? id, string? nationalities, string? nameForename)
        {
            int pageSize = 15;
            int pageNumber = page ?? 1; 

            var notices = await _repository.GetAllNoticesAsync(id, nationalities, nameForename);

            var pagedNotices = notices.ToPagedList(pageNumber, pageSize);

            return Ok(pagedNotices);
        }

    }
}
