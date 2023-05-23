using front1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace front1.Controllers
{
    public class CarController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7148/api");
        private readonly HttpClient _httpClient;
        public CarController()
        {
            _httpClient = new HttpClient(); 
            _httpClient.BaseAddress = baseAdress;   
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<CarModel> cars=new List<CarModel>();
            HttpResponseMessage responseMessage=_httpClient.GetAsync(_httpClient.BaseAddress+"/Cars/get_all_cars").Result;  
            if(responseMessage.IsSuccessStatusCode)
            {
                string data=responseMessage.Content.ReadAsStringAsync().Result;
                cars=JsonConvert.DeserializeObject<List<CarModel>>(data);

            }
            return View(cars);
        }

        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Create( CarModel model) {
            string data=JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data,Encoding.UTF8,"application/json");
            HttpResponseMessage response= _httpClient.PostAsync(baseAdress+"/Cars/create_car", content).Result;
            if(response.IsSuccessStatusCode)
            {
                
                return RedirectToAction("Index");
            }

            return View();
        
        }
        
    }
}
