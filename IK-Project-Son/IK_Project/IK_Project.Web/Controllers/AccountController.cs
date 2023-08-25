using Azure;
using IK_Project.Core.DTOs;
using IK_Project.Core.Entity;
using IK_Project.Entities.Enums;
using IK_Project.Web.Models.DTOs;
using IK_Project.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace IK_Project.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        const string uId = "userName";
        const string uRole = "userRole";
        const string uMail = "userMailAddress";

        string url = "https://localhost:7202";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            PersonelDTO personel = new PersonelDTO();
            using (var httpClient = new HttpClient())
            {
                using (var result = await httpClient.GetAsync($"{url}/api/Login?mail={dto.Email}&password={dto.Password}"))
                {
                    string apiCevap = await result.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
                    personel = JsonConvert.DeserializeObject<PersonelDTO>(data);
                }
            }
            if (personel != null)
            {
                HttpContext.Session.SetString(uId, personel.Id.ToString());
                HttpContext.Session.SetString(uMail, personel.Email.ToString());
                HttpContext.Session.SetString(uRole, personel.PersonStatus.ToString());

                var claims = new List<Claim>()
                    {
                        new Claim("FirstName",personel.FirstName),
                        new Claim("LastName",personel.LastName),
                        new Claim("Email",personel.Email),
                        new Claim("Photo",personel.Photo),
                        new Claim(ClaimTypes.Role,"Personel"),
                        new Claim("ID",personel.Id.ToString())

                    };
                if (personel.MiddleName != null)
                {
                    claims.Add(new Claim("MiddleName", personel.MiddleName));
                }
                if (personel.SecondLastName != null)
                {
                    claims.Add(new Claim("SecondLastName", personel.SecondLastName));
                }

                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                });
            }
            else
            {
                return View(dto);
            }

            return RedirectToAction("PersonelHomePages", "Account");
            //return RedirectToAction("PersonelHomePageUI", "Personel");
        }


        //--------------------------------------------- PERSONEL CONTROLLER -----------------------------------------
        public async Task<IActionResult> PersonelHomePages()
        {
            List<PersonelDTO> personels = new List<PersonelDTO>();
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{url}/api/Personel/All"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
                    personels = JsonConvert.DeserializeObject<List<PersonelDTO>>(data);
                }
            }
            var sessionId = Convert.ToInt32(HttpContext.Session.GetString(uId));


            var a = personels.Where(x => x.Id == sessionId).FirstOrDefault();
            //if (a.PersonStatus == Core.Enums.PersonStatus.admin)
            //{
            //    return View(a);

            //}
            //if (a.PersonStatus == Core.Enums.PersonStatus.staff)
            //{
            //    return View(a);

            //}
            return View(a);



        }

        //---------------------------------------------

        [HttpGet]
        public async Task<IActionResult> PersonelDetails(int id)
        {
            PersonelDTO personels = new();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{url}/api/Personel/GetById/{id}"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
                    personels = JsonConvert.DeserializeObject<PersonelDTO>(data);
                }
            }


            return View(personels);
        }

        [HttpGet]
        public async Task<IActionResult> PersonelUpdate(int id)
        {
            PersonelDTO personels = new();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{url}/api/Personel/GetById/{id}"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
                    personels = JsonConvert.DeserializeObject<PersonelDTO>(data);
                }
            }


            return View(personels);
        }

        [HttpPost]
        public async Task<IActionResult> PersonelUpdate(PersonelDTO asd)
        {
            PersonelDTO personels = new();
            PersonelDTO oldPersonel = new();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{url}/api/Personel/GetById/{asd.Id}"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
                    oldPersonel = JsonConvert.DeserializeObject<PersonelDTO>(data);
                }
            }
            oldPersonel.PhoneNumber = asd.PhoneNumber;


            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(oldPersonel), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{url}/api/Personel/",content))
                {
                    //cevap.StatusCode
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    //Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    //string data = json.SelectToken("data").ToString();
                    //personels = JsonConvert.DeserializeObject<PersonelDTO>(data);
                }
            }


            return View(oldPersonel);
        }



        [HttpGet]
        public async Task<IActionResult> CompanyList()
        {
            List<CompanyDTO> companies = new();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{url}/api/Company/All"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
                    companies = JsonConvert.DeserializeObject<List<CompanyDTO>>(data);
                }
            }
            return View(companies);

        }

        [HttpGet]
        public async Task<IActionResult> ManagersList()
        {
            List<PersonelDTO> managers = new();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{url}/api/Personel/GetManagers"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
                    managers = JsonConvert.DeserializeObject<List<PersonelDTO>>(data);
                }
            }
            return View(managers);
        }


        [HttpGet]
        public async Task<IActionResult> AddCompany()
        {
            CompanyWithPersonelDto company = new();
            List<PersonelDTO> personels = new();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{url}/api/Personel/All"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
                    personels = JsonConvert.DeserializeObject<List<PersonelDTO>>(data);
                }
            }
            company.Personels = personels;
            ViewBag.Manager = company;
            return View(company);
        }
        [HttpPost]
        public async Task<IActionResult> AddCompany(CompanyWithPersonelDto data)
        {
            CompanyDTO company = new();
            
            company.Name = data.Name;
            company.Title = data.Title;
            company.MersisNumber = data.MersisNumber;
            company.TaxNumber = data.TaxNumber;
            company.TaxAdministration = data.TaxAdministration;
            company.PhoneNumber = data.PhoneNumber;
            company.Address = data.Address;
            company.Email = data.Email;
            company.NumberOfEmployees = data.NumberOfEmployees;
            company.FoundationYear = data.FoundationYear;
            company.Logo = data.Logo;

            ViewBag.Manager = company;

            using (var httpClient = new HttpClient())
            {
                var serializeCompany = JsonConvert.SerializeObject(data);
                var content = new StringContent(serializeCompany, Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{url}/api/Company/Save", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    company = JsonConvert.DeserializeObject<CompanyDTO>(apiCevap);

                }
            }
            return View(company);
        }
        [HttpGet]
        public async Task<IActionResult> ExpenseRequest(ExpenseType expenseType)
        {
            var personelId = HttpContext.User.FindFirst("ID")?.Value;
            Personel personel = new();
            using (var httpClient = new HttpClient())
            {
                using (var result = await httpClient.GetAsync($"{url}/api/Personel/GetPersonelIncludeExpense/1"))
                {
                    if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        TempData["Exception"] = "Something went wrong!";
                        return View();
                    }
                    string response = await result.Content.ReadAsStringAsync();
                    personel = JsonConvert.DeserializeObject<Personel>(response);
                }
            }
            ExpenseRequestVM expenseVM = new();
            ExpenseRequest expenseRequest = new();
            expenseVM.Personel = personel;
            expenseVM.ExpenseRequest = expenseRequest;

            List<ExpenseType> expenseStatus = new();

            foreach (var item in Enum.GetValues(typeof(ExpenseType)))
            {
                expenseStatus.Add((ExpenseType)item);
            }
            foreach (ExpenseRequest item in personel.Expenses)
            {
                if (item.Status == ExpenseStatus.Pending)
                {
                    expenseStatus.Remove(item.ExpenseType);
                }
            }

            ViewBag.SelectList = expenseStatus;
            return View(expenseVM);
        }

    
    

        [HttpPost]
        public async Task<IActionResult> CreateRequest(ExpenseRequestVM expenseVM)
        {
            Personel personel = new Personel();

            ExpenseRequest expense = new();
            expense = expenseVM.ExpenseRequest;

            expense.RequestDate = DateTime.Now;

            using (var httpClient = new HttpClient())
            {
                using (var result = await httpClient.GetAsync($"{url}/api/Personel/GetById/{expenseVM.Personel.Id}"))
                {
                    if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        TempData["Exception"] = "Something went wrong!";
                        return View();
                    }
                    string apiCevap = await result.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
                    personel = JsonConvert.DeserializeObject<Personel>(data);

                }
            }

            using (var httpClient = new HttpClient())
            {
				StringContent content = new StringContent(JsonConvert.SerializeObject(personel), Encoding.UTF8, "application/json");

				using (var result = await httpClient.PutAsync($"{url}/api/Personel/Update/{personel.Id}", content))
                {
                    
                    string apiCevap = await result.Content.ReadAsStringAsync();
                    //Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    //string data = json.SelectToken("data").ToString();
                    //expense = JsonConvert.DeserializeObject<ExpenseRequestVM>(data);

                }
            }

            TempData["Message"] = "Expense request submitted successfully.";
            return RedirectToAction("ExpenseRequestList");
        }
        [HttpGet]
        public async Task<IActionResult> ExpenseRequestList()
        {
			Personel personel = new();

			//List<ExpenseRequestVM> expenseList = new();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{url}/api/Personel/GetPersonelIncludeExpense/{personel.Id}"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
                    personel = JsonConvert.DeserializeObject<Personel>(data);
                }
            }
            return View(personel);
        }
        [HttpGet]
        public async Task<IActionResult> LeaveRequestList()
        {
            List<LeaveRequestVM> leaveList = new();
			Personel personel = new();

			using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{url}/api/Personel/GetPersonelIncludeLeave/{personel.Id}"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(apiCevap);
                    string data = json.SelectToken("data").ToString();
					personel = JsonConvert.DeserializeObject<Personel>(data);
                }
            }
            return View(personel);


        }
		[HttpGet]
		public async Task<IActionResult> LeaveRequest(LeaveType leaveType)
		{
			
				var personelId = HttpContext.User.FindFirst("ID")?.Value;
				Personel personel = new();
				using (var httpClient = new HttpClient())
				{
					using (var result = await httpClient.GetAsync($"{url}/api/Personel/GetPersonelIncludeLeave/{leaveType}"))
					{
						if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
						{
							TempData["Exception"] = "Something went wrong!";
							return View();
						}
						string response = await result.Content.ReadAsStringAsync();
						personel = JsonConvert.DeserializeObject<Personel>(response);
					}
				}
				LeaveRequestVM expenseVM = new();
				Leave leave = new();
				expenseVM.Personel = personel;
				expenseVM.Leave = leave;

				List<LeaveType> leaveStatus = new();

				foreach (var item in Enum.GetValues(typeof(LeaveType)))
				{
				    leaveStatus.Add((LeaveType)item);
				}
				foreach (Leave item in personel.Leaves)
				{
					if (item.Status == LeaveStatus.Pending)
					{
						leaveStatus.Remove(item.LeaveType);
					}
				}

				ViewBag.SelectList = leaveStatus;
				return View(expenseVM);

			
		}
		[HttpPost]
        public async Task<IActionResult> LeaveRequest(LeaveRequestVM leaveRequestVM)
        {
            Personel personel = new();
            Leave leave = new();
            leave = leaveRequestVM.Leave;
            leave.RequestDate = DateTime.Now;

            using (var httpClient = new HttpClient())
            {
                using (var result = await httpClient.GetAsync($"{url}/api/Personel/GetByID/{leaveRequestVM.Personel.Id}"))
                {
                    if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        TempData["Exception"] = "Something went wrong!";
                        return View();
                    }
                    string response = await result.Content.ReadAsStringAsync();
                    personel = JsonConvert.DeserializeObject<Personel>(response);
                }
            }
            if (leaveRequestVM.Leave.LeaveType == LeaveType.Annual)
            {
                personel.UsedAnnualLeaveDays += leave.NumberOfLeaveDays;
            }
            personel.Leaves.Add(leave);

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(personel), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{url}/api/Personel/Update/{personel.Id}", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }
			return RedirectToAction("LeaveRequestList");

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

                using (var cevap = await httpClient.PostAsync($"{url}/api/Personel/Save", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("PersonelHomePages");
        }

    }
}
