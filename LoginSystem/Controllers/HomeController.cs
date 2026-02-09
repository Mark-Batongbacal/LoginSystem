using System.Diagnostics;
using LoginSystem.Models;
using LoginSystem.Models.Database;
using LoginSystem.Repository.Users;
using LoginSystem.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IUserDataManager _userDataManager;
        private readonly UserManager _userManager;
        public HomeController(IUserDataManager userDataManager)
        {
            _userManager = new UserManager(userDataManager);
        }

        public async Task<IActionResult> AddNewUser()
        {
            var user = new User()
            {
                UserName = "testsdjafh",
                Email = "tesasasasddasdddta@gmail.com",
                Password = "Pasasdsword"
            };

            var userDetails = new UserDetail
            {
                FirstName = "Testasd",
                LastName = "Testerasd",
                Address = "Test Addrsadess",
                Age = 25,
                Gender = "M"
            };
            try
            {
                await _userManager.CreateUserAsync(user, userDetails);
                return Ok("Added");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);

            }
        }

        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userManager.GetUserAsync(id);
            return Ok(user);
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
    }
}
