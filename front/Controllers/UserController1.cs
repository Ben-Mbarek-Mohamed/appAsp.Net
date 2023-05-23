using front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;

namespace front.Controllers
{
    public class UserController1 : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor ca;
        Uri baseAdress = new Uri("https://localhost:7148/api");
        public UserController1(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
            this.ca = httpContextAccessor;


        }


        [HttpGet]
        public IActionResult Index()
        {
            List<UserModel> users = new List<UserModel>();
            HttpResponseMessage response = _httpClient.GetAsync(baseAdress + "/User/get_all_users").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<UserModel>>(data);
            }
            return View(users);


        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            string encodedEmail = HttpUtility.UrlEncode(user.Email);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://localhost:7148/api/User/get_user_by_email/" + user.Email);
            HttpWebResponse webresponse = null;
            try
            {
                webresponse = request.GetResponse() as HttpWebResponse;

                ViewBag.Notification = "User exists";
                return View();
            }
            catch (WebException ex)
            {

                if (ex.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.NotFound)
                {

                    string data = JsonConvert.SerializeObject(user);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage createResponse = _httpClient.PostAsync(_httpClient.BaseAddress + "/User/create_user", content).Result;
                    if (createResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }
                }
                else
                {

                    ViewBag.Notification = "Error: " + ex.Message;
                    return View();
                }
            }

            webresponse.Close();
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            string encodedEmail = HttpUtility.UrlEncode(user.Email);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://localhost:7148/api/User/get_user_by_email/" + user.Email);
            HttpWebResponse webresponse = null;
            try
            {
                webresponse = request.GetResponse() as HttpWebResponse;
                UserModel user1 = new UserModel();
                HttpResponseMessage response = _httpClient.GetAsync(baseAdress + "/User/get_user_by_email/" + user.Email).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    user1 = JsonConvert.DeserializeObject<UserModel>(data);
                }
                if (user1.Password == user.Password)
                {
                    ViewBag.Message = "login successful";
                    ca.HttpContext.Session.SetString("userName", user1.Name);
                    ca.HttpContext.Session.SetString("Role", user1.Role.ToString());
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.Message = "Wrong password";
                    return View();

                }


            }
            catch (WebException ex)
            {

                if (ex.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.NotFound)
                {

                    ViewBag.Message = "wrong email";
                    return View();
                }

                else
                {

                    ViewBag.Message = "Error: " + ex.Message;
                    return View();
                }

            }

            webresponse.Close();
            return View();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            UserModel user = new UserModel();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/User/get_user_by_id/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content?.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserModel>(data);
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            string data = JsonConvert.SerializeObject(user);

            HttpResponseMessage response = _httpClient.PutAsJsonAsync<UserModel>(_httpClient.BaseAddress + "/User/update_user/" + user.Id, user).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            UserModel user = new UserModel();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/User/get_user_by_id/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserModel>(data);

            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/User/delete_user/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(); 
        }

        public IActionResult Logout()
        {
            ca.HttpContext.Session.Clear();
            return RedirectToAction("Login");

        }

    }

}


