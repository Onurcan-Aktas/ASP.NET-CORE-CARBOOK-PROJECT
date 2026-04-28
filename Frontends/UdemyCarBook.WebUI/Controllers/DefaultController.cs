using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UdemyCarBook.Dto.LocationDtos;

namespace UdemyCarBook.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Olası null hatalarını önlemek için varsayılan olarak boş bir liste oluşturuyoruz
            List<SelectListItem> values2 = new List<SelectListItem>();

            var token = User.Claims.FirstOrDefault(x => x.Type == "carbooktoken")?.Value;

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync("http://localhost:5243/api/Locations");

                // API'den başarılı yanıt gelip gelmediğini kontrol etmek her zaman iyidir
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);

                    values2 = (from x in values
                               select new SelectListItem
                               {
                                   Text = x.Name,
                                   Value = x.LocationID.ToString()
                               }).ToList();
                }
            }

            // ÇÖZÜM NOKTASI: View tarafında aranan isimle (LocationID) birebir aynı atamayı yapıyoruz
            ViewBag.v = values2;

            return View();
        }

        [HttpPost]
        public IActionResult Index(string book_pick_date, string book_off_date, string time_pick, string time_off, string locationID)
        {
            TempData["bookpickdate"] = book_pick_date;
            TempData["bookoffdate"] = book_off_date;
            TempData["timepick"] = time_pick;
            TempData["timeoff"] = time_off;
            TempData["locationID"] = locationID;

            return RedirectToAction("Index", "RentACarList");
        }
    }
}