using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Atyati.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Atyati.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
 
        }
        Employee db = new Employee();

        [HttpGet]
        public IActionResult Login()
        {

            return View();
            
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginDetails)
        {
            if (ModelState.IsValid)
            {
               // db.GetAllEmployees = HttpContext.Session.GetObject("empinfo");

                var obj = _employeeRepository.GetAllEmployee().Where(a => a.Email.Equals(loginDetails.Email) && a.Password.Equals(loginDetails.Password)).FirstOrDefault();
                if (obj != null)
                {
                    //ViewData["UserID"] = obj.EmployeeId.ToString();
                    //ViewData["UserName"] = obj.Email.ToString();
                    HttpContext.Session.SetString("username", obj.EmployeeId.ToString());
                    return RedirectToAction("Home");
                }

            }


            ModelState.AddModelError(string.Empty,"Invalid UserName or Password");
            return View();
        }
  
        [HttpGet]
        public IActionResult Home()
        {

          
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("Login");
            }
            ViewBag.Title = "HOME";

           var data = _employeeRepository.GetAllEmployee();
           // Console.WriteLine(data[0].Department);
           
            return View(_employeeRepository.GetAllEmployee());

        }



        public IActionResult Details(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("Login");
            }
            ViewBag.Title = "Details";
           
            var emp = _employeeRepository.GetAllEmployee().SingleOrDefault(x => x.EmployeeId == id);

            return View(emp);
        
        }


        public IActionResult Index()
        {
            //var objComplex = db.GetAllEmployees;
            //db.GetAllEmployees = HttpContext.Session.GetObject("empinfo");
            //var data = db.GetAllEmployees.OrderByDescending(a => a.EmployeeId);
            //HttpContext.Session.SetObject("empinfo", db.GetAllEmployees);

            return Json(new { data = _employeeRepository.GetAllEmployee() });

            

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Edit";
           
            Employee employee =
                   _employeeRepository.GetAllEmployee().Single(emp => emp.EmployeeId == id);

            return View(employee);

          
        }
        [HttpPost]
        public IActionResult Edit(Employee e)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Update(e);

                return View("Home");
            }
    //        ViewBag.values = new List<SelectListItem>
    //{
    //    new SelectListItem { Value = "IT", Text = "IT" },
    //    new SelectListItem { Value = "HR", Text = "HR" },
    //    new SelectListItem { Value = "PMO", Text = "PMO"  },
    //};
            ModelState.AddModelError(string.Empty, "Fill Mandatory fields");
            //return RedirectToAction("Edit",new {id=e.EmployeeId });
            return View(e);
        }



        [HttpGet]
        public IActionResult Create()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                
                return RedirectToAction("Login");
            }
            //var empSession = HttpContext.Session.GetObject("empinfo");
            ViewBag.values = new List<SelectListItem>
    {
        new SelectListItem { Value = "IT", Text = "IT" },
        new SelectListItem { Value = "HR", Text = "HR" },
        new SelectListItem { Value = "PMO", Text = "PMO"  },
    };
            return View();

        }

        [HttpPost]
        public IActionResult Create(Employee e)
        {
            if (ModelState.IsValid)
            {
                var mail = _employeeRepository.GetAllEmployee().FirstOrDefault(x => x.Email == e.Email);
                if (mail != null)
                {
                    ViewBag.values = new List<SelectListItem>
    {
        new SelectListItem { Value = "IT", Text = "IT" },
        new SelectListItem { Value = "HR", Text = "HR" },
        new SelectListItem { Value = "PMO", Text = "PMO"  },
    };

                    ModelState.AddModelError(string.Empty, "Email ID  alredy exists");
                    return View(e);
                }
                _employeeRepository.Add(e);

                return RedirectToAction("Home");
            }
            ModelState.AddModelError(string.Empty, "Fill Mandatory fields");
            return RedirectToAction("Create");
        }



        public IActionResult Delete(int id)
        {
            _employeeRepository.Delete(id);
            return RedirectToAction("Home"); ;



        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {

            HttpContext.Session.SetString("username",string.Empty);
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
