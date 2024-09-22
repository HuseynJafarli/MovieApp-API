using Microsoft.AspNetCore.Mvc;
using MovieApp.MVC.ApiResponseMessages;
using MovieApp.MVC.Models;
using MovieApp.MVC.ViewModels.GenreVMs;
using RestSharp;
using System.Diagnostics;

namespace MovieApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestClient restClient;

        public HomeController()
        {
            restClient = new RestClient("https://localhost:7159/api/");
        }
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("Genres",Method.Get);
            var response = await restClient.ExecuteAsync<ApiResponseMessage<ICollection<GenreGetVM>>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            return View(response.Data.Data);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var request = new RestRequest($"Genres/{id}", Method.Get);
            var response = await restClient.ExecuteAsync<ApiResponseMessage<GenreGetVM>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            return View(response.Data.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateVM vm)
        {
            var request = new RestRequest($"Genres", Method.Post);
            request.AddJsonBody(vm);

            var response = await restClient.ExecuteAsync<ApiResponseMessage<GenreCreateVM>>(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("Name", response.Data.ErrorMessage);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
