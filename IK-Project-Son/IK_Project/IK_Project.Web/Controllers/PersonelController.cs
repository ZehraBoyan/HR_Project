using AutoMapper;
using IK_Project.Core.DTOs;
using IK_Project.Core.Entity;
using IK_Project.Core.Repositories;
using IK_Project.Core.Services;
using IK_Project.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;

namespace IK_Project.Web.Controllers
{
    [Authorize(Roles = "Personel")]

	public class PersonelController : Controller
	{
        string url = "https://localhost:7202";

        public ActionResult PersonelHomePages()
        {
            // Bu eylemi gerçekleştirmek için kod ekleyin.
            return RedirectToAction("PersonelHomePage","Personel");
        }
        public async Task<IActionResult> PersonelHomePageUI()
        {
			List<Personel> personels = new List<Personel>();
			using (var httpClient = new HttpClient())
			{
				using (var cevap = await httpClient.GetAsync($"{url}/api/Personel/All"))
				{
					string apiCevap = await cevap.Content.ReadAsStringAsync();
					Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
					string data = json.SelectToken("data").ToString();
					personels = JsonConvert.DeserializeObject<List<Personel>>(data);
				}
			}
			//Personel personel = personels[0];
			//var personelSummaryVM = _personelService.GetAllAsync();
			//personelSummaryVM.FullName = $"{personel.FirstName} {personel.MiddleName} {personel.LastName} {personel.SecondLastName}";

			//ViewBag.PersonelId = personel.Id;
			//ViewBag.PersonelPhoto = personel.Photo;

			return View(personels);
		}

        [HttpGet]
        public IActionResult AddPersonel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPersonel(Personel personel)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(personel), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PostAsync($"{url}/api/Personel/AddPersonel", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
       
        
    }
}
