using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class CarController1 : Controller
    {
        private readonly HttpClient _httpClient;
        Uri baseAdress = new Uri("https://localhost:7148/api");
        public CarController1()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;

        }
        public IActionResult Index()
        {
            List<CarModel> cars = new List<CarModel>();
            HttpResponseMessage response = _httpClient.GetAsync(baseAdress + "/Cars/get_all_cars").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cars = JsonConvert.DeserializeObject<List<CarModel>>(data);
            }
            return View();
        }
    }
}
