using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.BlogDtos;
using UdemyCarBook.Dto.TagCloudDtos;
using System.Collections.Generic; // List için gerekli

namespace UdemyCarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailsTagCloudComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _BlogDetailsTagCloudComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.blogid = id;
            var client = _httpClientFactory.CreateClient();

            // 1. DÜZELTME: URL'deki "GetTagClod" yazım yanlışını "GetTagCloud" olarak düzelttik.
            // (Eğer API tarafında gerçekten 'Clod' yazdıysan eski haline alabilirsin ama muhtemelen Cloud'dur)
            var responseMessage = await client.GetAsync($"http://localhost:5243/api/TagClouds/GetTagCloudByBlogId/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // 2. DÜZELTME: View tarafında foreach kullanıldığı için veriyi Liste (List<GetByBlogIdTagCloudDto>) olarak alıyoruz.
                var values = JsonConvert.DeserializeObject<List<GetByBlogIdTagCloudDto>>(jsonData);

                // Gelen veri null olsa bile (API boş döndürse bile) foreach'in çökmemesi için boş liste kontrolü
                if (values != null)
                {
                    return View(values);
                }
            }

            // 3. DÜZELTME: API hata verirse (404, 500 vb.) View() diyerek null göndermek yerine, 
            // boş bir liste gönderiyoruz. Böylece sayfa çökmüyor, sadece etiketler kısmı boş kalıyor.
            return View(new List<GetByBlogIdTagCloudDto>());
        }
    }
}