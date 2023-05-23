using front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace front.Controllers
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
        [HttpGet]
        public IActionResult Index()
        {
            List<CarModel> cars = new List<CarModel>();
            HttpResponseMessage response = _httpClient.GetAsync(baseAdress + "/Cars/get_all_cars").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cars = JsonConvert.DeserializeObject<List<CarModel>>(data);
            }
            return View(cars);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CarModel car)
        {
            string data = JsonConvert.SerializeObject(car);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Cars/create_car", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            CarModel car = new CarModel();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Cars/get_car_by_id/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content?.ReadAsStringAsync().Result;
                car = JsonConvert.DeserializeObject<CarModel>(data);
            }
            return View(car);
        }
        [HttpPost]
        public ActionResult Edit(CarModel car)
        {
            string data = JsonConvert.SerializeObject(car);

            HttpResponseMessage response = _httpClient.PutAsJsonAsync<CarModel>(_httpClient.BaseAddress + "/Cars/update_car/" + car.Id, car).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(car);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            CarModel car = new CarModel();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Cars/get_car_by_id/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                car = JsonConvert.DeserializeObject<CarModel>(data);

            }

            return View(car);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Cars/delete_car/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
