using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VigilantCore.Web.AspNet.DTOs;
using VigilantCore.Web.AspNet.Models;
using VigilantCore.Web.AspNet.Repositories;
using PagedList;

namespace VigilantCore.Web.AspNet.Controllers
{
    [ApiController]
    [Route("fbi")]
    public class WantedController : ControllerBase
    {
        private readonly IWantedRepository _repository;

        public WantedController(IWantedRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetWanted")]
        public async Task<IActionResult> GetWanted()
        {
            int currentPage = 1;
            int totalRecords = 0;
            List<WantedModel> newWantedList = new List<WantedModel>();
            await _repository.DeleteAllAsync();

            int delayBetweenCalls = 1000;

            using (HttpClient httpClient = new HttpClient())
            {
                do
                {
                    var response = await httpClient.GetStringAsync($"https://api.fbi.gov/wanted/v1/list?page={currentPage}");
                    var responseObject = JsonConvert.DeserializeObject<ResponseObject>(response); 
                    var items = responseObject.Items;
                    totalRecords = responseObject.Total;

                    newWantedList.Clear();

                    foreach (var dto in items)
                    {
                        string originalImage = dto.Images.FirstOrDefault()?.Original ?? "";

                        var newWanted = new WantedModel(
                            dto.Uid,
                            dto.Title,
                            dto.Locations,
                            dto.Sex,
                            dto.Nationality,
                            dto.Age_Min,
                            dto.Age_Max,
                            string.Join(",", dto.Subjects ?? new List<string>()),
                            originalImage,
                            dto.Url,
                            dto.Race,
                            dto.Place_Of_Birth
                        );

                        newWantedList.Add(newWanted);
                    }

                    await _repository.AddRangeAsync(newWantedList);

                    currentPage++;

                    await Task.Delay(delayBetweenCalls);

                } while (newWantedList.Count < totalRecords);
            }

            return Ok("Dados salvos com sucesso");
        }

        [HttpGet("GetAllWanted")]
        public async Task<IActionResult> GetAllWanted(
            int? page = 1,
            int pageSize = 15,
            int? id = null,
            string? title = null,
            string? subject = null,
            string? nationality = null,
            string? sex = null,
            string? race = null)
        {
            var wantedList = await _repository.GetAllWantedAsync(id, title, subject, nationality, sex, race);

            int pageNumber = page ?? 1;

            var pagedWantedList = wantedList.ToPagedList(pageNumber, pageSize);

            return Ok(pagedWantedList);
        }
    }
}
