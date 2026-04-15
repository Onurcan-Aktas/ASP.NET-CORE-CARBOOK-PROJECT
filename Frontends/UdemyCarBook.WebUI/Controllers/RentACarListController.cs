using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyCarBook.Dto.BrandDtos;
using UdemyCarBook.Dto.RentACarDtos;

namespace UdemyCarBook.WebUI.Controllers
{
    public class RentACarListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RentACarListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int id)
        {
            var locationID = TempData["locationID"];

            // NullReferenceException hatasını önlemek için kontrol ediyoruz
            if (locationID != null)
            {
                id = int.Parse(locationID.ToString());
            }

            // ViewBag atamasını güncel ve güvenli id değeri ile yapıyoruz
            ViewBag.locationID = id;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5243/api/RentACars?locationID={id}&available=true");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<FilterRentACarDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}