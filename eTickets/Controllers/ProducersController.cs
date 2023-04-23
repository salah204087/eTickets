using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
             _service = service;
        }
        public IActionResult Index()
        {
            var allProducers = _service.GetAll();
            return View(allProducers);
        }
        public IActionResult Details(int id)
        {
            var producerDetails=_service.GetById(id);
            if (producerDetails == null)
            {
                return View("NotFound");
            }
            return View(producerDetails);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("ProfilePictureURL,FullName,Bio")]Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
			_service.Add(producer);
			return RedirectToAction(nameof(Index));
	    }
		public IActionResult Edit(int id)
		{
            var producerDetails=_service.GetById(id);
            if (producerDetails == null)
                return View("NotFound");
			return View(producerDetails);
		}
		[HttpPost]
		public IActionResult Edit(int id ,Producer producer)
		{
			if (!ModelState.IsValid)
			{
				return View(producer);
			}
            if (id==producer.Id)
            {
				_service.Update(id, producer);
				return RedirectToAction(nameof(Index));
			}
			return View(producer);
		}
		public IActionResult Delete(int id)
		{
			var producerDetails = _service.GetById(id);
			if (producerDetails == null)
				return View("NotFound");
			return View(producerDetails);
		}
		[HttpPost,ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var producerDetails = _service.GetById(id);
			if (producerDetails == null)
				return View("NotFound");
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
		}
	}
}
