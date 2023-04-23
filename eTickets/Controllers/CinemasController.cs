using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var allCinemas = _service.GetAll();
            return View(allCinemas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Logo,Name,Description")]Cinema cinema)
        {
            if (!ModelState.IsValid) 
            {
                return View(cinema);
            }
            _service.Add(cinema);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id) 
        {
            var cinemaDetails=_service.GetById(id);
            if (cinemaDetails == null)
                return View("NotFound");
            return View(cinemaDetails);
        }
		public IActionResult Edit(int id)
		{
			var cinemaDetails = _service.GetById(id);
			if (cinemaDetails == null)
				return View("NotFound");
			return View(cinemaDetails);
		}
		[HttpPost]
		public IActionResult Edit(int id ,Cinema cinema)
		{
			if (!ModelState.IsValid)
			{
				return View(cinema);
			}
			_service.Update(id,cinema);
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Delete(int id)
		{
			var cinemaDetails = _service.GetById(id);
			if (cinemaDetails == null)
				return View("NotFound");
			return View(cinemaDetails);
		}
		[HttpPost,ActionName("Delete")]
		public IActionResult DeleteConfirm(int id)
		{
			var cinemaDetails = _service.GetById(id);
			if (cinemaDetails == null)
				return View("NotFound");
			_service.Delete(id);
			return RedirectToAction(nameof(Index));
		}
	} 
}
