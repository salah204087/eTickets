using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var allMovies=_service.GetAll(c=>c.Cinema);
            return View(allMovies);
        }
        public IActionResult Filter(string searchString)
        {
            var allMovies = _service.GetAll(c => c.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                if (filteredResult.Any())
                {
                    return View("Index", filteredResult);
                }
                else
                {
                    return View("Index", allMovies);
                }
            }
            return View("Index", allMovies);
        }
        public IActionResult Details(int id)
        {
            var movieDetail=_service.GetMovieById(id);
            return View(movieDetail);
        }
        public IActionResult Create()
        {
            var movieDropdownData = _service.GetNewMovieDropdownsValues();
            ViewBag.CinemaId = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.ProducerId = new SelectList(movieDropdownData.Producers, "Id", "FullName");
            ViewBag.ActorId = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownData = _service.GetNewMovieDropdownsValues();
                ViewBag.CinemaId = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.ProducerId = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                ViewBag.ActorId = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                return View(movie);
            }
            _service.AddNewMovie(movie);
            return RedirectToAction(nameof(Index)); 
        }
        public IActionResult Edit(int id)
        {
            var movieDetails=_service.GetMovieById(id);
            if (movieDetails == null)
                return View("NotFound");

            var response = new NewMovieVM()
            {
                Id=movieDetails.Id,
                Name=movieDetails.Name,
                Description=movieDetails.Description,
                StartDate=movieDetails.StartDate,
                EndDate=movieDetails.EndDate,
                Price=movieDetails.Price,
                MovieCategory=movieDetails.MovieCategory,
                ImageURL=movieDetails.ImageURL,
                CinemaId=movieDetails.CinemaId,
                ProducerId=movieDetails.ProducerId,
                ActorIds=movieDetails.Actors_Movies.Select(n=>n.ActorId).ToList(),
            };

            var movieDropdownData = _service.GetNewMovieDropdownsValues();
            ViewBag.CinemaId = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.ProducerId = new SelectList(movieDropdownData.Producers, "Id", "FullName");
            ViewBag.ActorId = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            return View(response);
        }
        [HttpPost]
        public IActionResult Edit(int id,NewMovieVM movie)
        {
            if (id !=movie.Id) 
            {
                return View("NotFound");
            
            }
            if (!ModelState.IsValid)
            {
                var movieDropdownData = _service.GetNewMovieDropdownsValues();
                ViewBag.CinemaId = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.ProducerId = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                ViewBag.ActorId = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                return View(movie);
            }
            _service.UpdateMovie(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
