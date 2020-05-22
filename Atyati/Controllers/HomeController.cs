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
using System.Collections;


namespace Atyati.Controllers
{
    public class HomeController : Controller
    {
        AtyatiContext Adb = new AtyatiContext();
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

         


            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("Login");
            }
            ViewBag.Title = "HOME";

           var data = _employeeRepository.GetAllEmployee();
            // Console.WriteLine(data[0].Department);
           ViewBag.TotalPrice = Chart().Select(x=>x.TotalPrice).ToList();
            ViewBag.Category = Chart().Select(x => x.CategoryName).ToList();
            ViewBag.Sum = Chart().Sum(x => x.TotalPrice);

            ViewBag.Product = _employeeRepository.GetAllSold().Select(z=>z.Name).ToList();
            ViewBag.ProductTotalPrice = _employeeRepository.GetAllSold().Select(z => (z.Price*z.Quantity)).ToList();
       

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
        public IActionResult GetAllProducts()
        {
            return View(_employeeRepository.GetAllProducts());
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

            Employees employee =
                   _employeeRepository.GetAllEmployee().Single(emp => emp.EmployeeId == id);

            return View(employee);

          
        }
        //public JsonResult getStudent(string id)
        //{
        //    List<Student> students = new List<Student>();
        //    students = context.Students.ToList();


        //    return Json(students, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public IActionResult UpdateStock(int id,bool val)
        {
           var p= _employeeRepository.GetAllProducts().SingleOrDefault(x => x.Pid == id);
            p.IsOutOfStock = val;
            if (val == true)
            {
                p.Quantity = 0;
            }
            _employeeRepository.Update(p);

            return View("GetAllProducts", _employeeRepository.GetAllProducts());
        }

        public IActionResult GetAllProductsAPI()
        {

            return Json(new { data = _employeeRepository.GetAllProducts() });

        }

        [HttpPost]
        public IActionResult Edit(Employees e)
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
        public IActionResult Create(Employees e)
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
        [HttpGet]
        public IActionResult  CreateProduct()
        {
            
            var model = new Product();

            var c = _employeeRepository.GetAllProducts().SingleOrDefault(x => x.Pid == 1).Category;
            model.Category = c;
            ViewBag.values = new SelectList(Adb.Category, "CategoryId", "CategoryName"); 
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(Product prd)
        {
            _employeeRepository.AddProduct(prd);
            return View("GetAllProducts", _employeeRepository.GetAllProducts());
            //var c = Adb.Category;
            //ViewBag.Category = new SelectList(Adb.Category, "CategoryId", "CategoryName");
            //return View();
        }



 
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
           var model= _employeeRepository.GetAllProducts().Single(x => x.Pid == id);

            ViewBag.values = new SelectList(Adb.Category, "CategoryId", "CategoryName");
            return View(model);
            //var c = Adb.Category;
            //ViewBag.Category = new SelectList(Adb.Category, "CategoryId", "CategoryName");
            //return View();
        }
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Update(product);
                return View("GetAllProducts", _employeeRepository.GetAllProducts());
            }
            else
            {
                ViewBag.values = new SelectList(Adb.Category, "CategoryId", "CategoryName");
                return View(product);
            }
            //var c = Adb.Category;
            //ViewBag.Category = new SelectList(Adb.Category, "CategoryId", "CategoryName");
            //return View();
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
        [HttpGet]
        public IActionResult GetSales()
        {
            ViewBag.Sum = Chart().Sum(x => x.TotalPrice);

            return View(_employeeRepository.GetAllSold());
        }
       
        [HttpGet]
        public IActionResult GetSales2()
        {

            var tdata = _employeeRepository.GetAllTemp();
            ViewModel vmd = new ViewModel();


            vmd.ProductsToBeSold = _employeeRepository.GetAllProducts();
            vmd.ProductsInCart = tdata;
            return View("GetSales2", vmd);


            //return View("GetSales2", vm);
        }
        [HttpPost]
        public void GetSales3(IEnumerable<TempSales> vm)
        {
            int totalPrice = 0;
            List<TempSales> cartList = new List<TempSales>();
            ViewModel vmd = new ViewModel();
            foreach (var v in vm)
            {
                     var ep=    _employeeRepository.GetAllTemp().SingleOrDefault(c => c.Pid == v.Pid);
                if (ep == null)
                {
                    var cartProd = _employeeRepository.GetAllProducts().Single(c => c.Pid == v.Pid);
                    cartProd.Quantity = v.Quantity;
                    //cartList.Add(cartProd);
                    //  totalPrice = totalPrice + ((int)cartProd.Price * (int)cartProd.Quantity);
                    var tData = Converter(cartProd);
                    _employeeRepository.AddTemp(tData);
                }
                else{

                }

            }
            //ViewBag.TotalPrice = totalPrice;

            ////_employeeRepository.Add(cartList);
            //vmd.ProductsToBeSold = _employeeRepository.GetAllProducts();
            //vmd.ProductsInCart = _employeeRepository.GetCart();
            //GetSales2(vmd, totalPrice);

            //return View("GetSales2", vmd);
        }

  

        public static TempSales Converter(Product p)
        {
            return new TempSales
            {
                Pid = p.Pid,
                Name = p.Name,
                Price = p.Price,
                Brand = p.Brand,
                Quantity = p.Quantity,
                CategoryId = p.CategoryId,
                IsOutOfStock = p.IsOutOfStock,
                Category = p.Category
            };
        }

        public static Sold Converter(TempSales p)
        {
            return new Sold
            {
                Pid = p.Pid,
                Name = p.Name,
                Price = p.Price,
                Brand = p.Brand,
                Quantity = p.Quantity,
                CategoryId = p.CategoryId,
             
                Category = p.Category
            };
        }

        [HttpPost]
        public void ConfirmSale(TempSales tempsales)
        {
            var sData = _employeeRepository.GetAllTemp().Single(x=>x.Pid== tempsales.Pid);
            _employeeRepository.AddSales(Converter(sData));
            _employeeRepository.DeleteTemp(sData.Pid);
            //var sData = _employeeRepositor.Ge .Single(x => x.Pid == tempsales.Pid);
            var p=_employeeRepository.GetAllProducts().Single(x => x.Pid == tempsales.Pid);
            p.Quantity = p.Quantity - sData.Quantity;
            if (p.Quantity == 0)
            {
                p.IsOutOfStock = true;

            }
            _employeeRepository.Update(p);
            GetSales();
        }


        [HttpPost]
        public void Reject(TempSales tempsales)
        {
            var sData = _employeeRepository.GetAllTemp().Single(x => x.Pid == tempsales.Pid);

            _employeeRepository.DeleteTemp(sData.Pid);
            GetSales();
        }
        
       public List<GetStats> Chart()
        {
            List<GetStats> al = new List<GetStats>();
            
        var GrpData  =  from data in _employeeRepository.GetAllSold()
            group data by data.Category.CategoryName into eGroup
            orderby eGroup.Key
            select new 
            {
                Category= eGroup.Key,
                subData= eGroup.OrderBy(x=>x.Price)

            };

            foreach(var grp in GrpData )
            {
                GetStats gt = new GetStats();
                gt.CategoryName  = grp.Category;
                foreach (var p in grp.subData)
                {
                   gt.TotalPrice= gt.TotalPrice+Convert.ToInt32(p.Price * p.Quantity);

                }
                al.Add(gt);

            }
            Console.WriteLine(al);
            return al;
        }

    }
}
