using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UdemyCarBook.Dto.FooterAdressDtos;

namespace UdemyCarBook.WebUI.ViewComponents.FooterAdressComponents
{
    public class _FooterAdressComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FooterAdressComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // DİKKAT: Api'nin doğru çalıştığı URL'yi kullanıyoruz
            var responseMessage = await client.GetAsync("http://localhost:5243/api/FooterAddresses");

            responseMessage.EnsureSuccessStatusCode(); // Eğer API hata verirse sayfa bilerek çöksün ve görelim

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFooterAdressDto>>(jsonData);

            return View(values);
        }
    }
}