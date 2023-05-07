using ImageShareLikesEF.Data;
using ImageShareLikesEF.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;


namespace ImageShareLikesEF.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        private IWebHostEnvironment _webHostEnvironment;
        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var repo = new ImageRepository(_connectionString);
            var vm = new HomePageViewModel();
            vm.Images = repo.GetImages();
            return View(vm);
        }
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(string title, IFormFile imageFile)
        {
            var fileName = $"{Guid.NewGuid()}-{imageFile.FileName}{Path.GetExtension(imageFile.FileName)}";
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
            using var stream = new FileStream(fullPath, FileMode.CreateNew);
            imageFile.CopyTo(stream);

            var image = new Image
            {
                Title = title,
                FileName = fileName,
                UploadedDate = DateTime.Now
            };
            var repo = new ImageRepository(_connectionString);
            repo.Add(image);

            return RedirectToAction("Index");
        }
        public IActionResult ViewImage(int id)
        {
            var repo = new ImageRepository(_connectionString);
            var image = repo.GetImageById(id);
            if (image == null)
            {
                return RedirectToAction("Index");
            }
            var vm = new ImageViewModel
            {
                Image = image
            };
            if (HttpContext.Session.GetString("likedids") != null)
            {
                var likedIds = HttpContext.Session.Get<List<int>>("likedids");
                vm.CanLikeImage = likedIds.All(i => i != id);
            }
            else
            {
                vm.CanLikeImage = true;
            }

            return View(vm);
        }
        [HttpPost]
        public void UpdateImageLikes(int id)
        {
            var repo = new ImageRepository(_connectionString);
            repo.UpdateLikes(id);
            List<int> likedIds = HttpContext.Session.Get<List<int>>("likedids") ?? new List<int>();
            likedIds.Add(id);
            HttpContext.Session.Set("likedids", likedIds);
        }
        public ActionResult GetLikes(int id)
        {
            var repo = new ImageRepository(_connectionString);
            return Json(new { Likes = repo.GetLikes(id) }); 
        }

    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}