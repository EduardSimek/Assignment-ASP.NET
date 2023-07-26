using Microsoft.AspNetCore.Mvc;
using MyApp.Interfaces;
using MyApp.Models;
using System.Diagnostics;

namespace MyApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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


        public IActionResult SearchUser(string searchTerm, string sorting)
        {
            List<Users> users = new List<Users>();

            using (var db = new AnotherClass())
            {
                users = db.Users.Where(u => u.FirstName.ToLower().Contains(searchTerm.ToLower())).ToList();
                switch (sorting)
                {
                    case "firstName":
                        users = users.OrderBy(u => u.FirstName).ToList();
                        break;
                    case "age":
                        users = users.OrderBy(u => u.Age).ToList();
                        break;
                    default:
                        break;
                }
            }
            TempData["users"] = users;
            return View("RetrieveAllUsers");
        }

        public IActionResult DeleteUser(int ID)
        {
            using (var db = new AnotherClass())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == ID);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }

            }
            TempData["DeleteMessage"] = "User was successfully deleted.";
            return RedirectToAction("AddUsers");

            //return Json (new {message = "User was successfully deleted."});
        }

        
        public IActionResult RetrieveAllUsers()
        {

            List<Users> users = new List<Users>();

            using (var db = new AnotherClass())
            {
                users = db.Users.ToList();
            }

            TempData["users"] = users;

            return View();

        }

        public IActionResult AddUsers()
        {

            List<Users> users = new List<Users>();

            using (var db = new AnotherClass())
            {
                users = db.Users.ToList();
            }

            TempData["users"] = users;

            return View();
        }


        /* [HttpPost]
         public IActionResult AddUsers(Users user)
         {
             if (ModelState.IsValid)
             {
                 using (var db = new AnotherClass())
                 {
                     db.Add(user);
                     db.SaveChanges();
                 }
             }

             TempData["addMessage"] = "User was successfully added to database.";
             return View();
         }
        */

         [HttpPost]
         public IActionResult AddUsers(Users user)
         {
             if (ModelState.IsValid)
             {
                 using (var db = new AnotherClass())
                 {
                     var existingUser = db.Users.FirstOrDefault(u => 
                             u.Id == user.Id && 
                             u.FirstName == user.FirstName && 
                             u.LastName == user.LastName && 
                             u.PhoneNumber == user.PhoneNumber && 
                             u.Email == user.Email);

                     if (existingUser != null)
                     {
                         ModelState.AddModelError(string.Empty, "A user with the same first name, last name, email address and phone number already exists.");
                         return View(user);
                     }

                     db.Add(user);
                     db.SaveChanges();
                 }
                TempData["addMessage"] = "User was successfully added to the database.";
                return RedirectToAction("AddUsers");
            }
            return View(user);

             
         }
        

        [HttpPost]
        public IActionResult UpdateUser(Users user)
        {

            using (var db = new AnotherClass())
            {
                var userTemp = db.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                TempData["userTemp"] = userTemp;
            }

            //TempData["users"] = users;
            //return View("RetrieveAllUsers");
            return View();
        }

        public IActionResult UpdateUserFinal(Users user)
        {
            using (var db = new AnotherClass())
            {
                //var updateUser = db.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                var updateUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

                if (updateUser != null)
                {
                    updateUser.FirstName = user.FirstName;
                    updateUser.LastName = user.LastName;
                    updateUser.PhoneNumber = user.PhoneNumber;
                    updateUser.Id = user.Id;
                    updateUser.Age = user.Age;
                    updateUser.Email = user.Email;

                    db.SaveChanges();

                    TempData["UpdateMessage"] = "User was successfully updated.";
                } 
     
            }

            return RedirectToAction("AddUsers");

            //return View("AddUsers");
        }

        public class MyController: Controller   //using DI
        {
            private readonly IClassDBContext _dbContext;

            public MyController(IClassDBContext dbContext)
            {
                _dbContext = dbContext;
            }
        }
        

    }
}