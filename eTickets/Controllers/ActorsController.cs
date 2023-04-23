using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var data = _service.GetAll();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("ProfilePictureURL,FullName,Bio")]Actor actor)
        {
            if (!ModelState.IsValid) 
            {
                return View(actor);
            }
            _service.Add(actor);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id) 
        {
            var actorDetails=_service.GetById(id);
            if (actorDetails == null)
                return View("NotFound");
            return View(actorDetails);
        }
        public IActionResult Edit(int id)
        {
            var actorDetails = _service.GetById(id);
            if (actorDetails == null)
                return View("NotFound");
            return View(actorDetails);
        }
        [HttpPost]
        public IActionResult Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            _service.Update(id,actor);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var actorDetails = _service.GetById(id);
            if (actorDetails == null)
                return View("NotFound");
            return View(actorDetails);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var actorDetails = _service.GetById(id);
            if (actorDetails == null)
                return View("NotFound");
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
