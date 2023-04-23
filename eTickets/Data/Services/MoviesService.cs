using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eTickets.Data.Services
{
	public class MoviesService:EntityBaseRepository<Movie>, IMoviesService
	{
		private readonly AppDbContext _context;
        public MoviesService(AppDbContext context):base(context)
        {
            _context = context;
        }

        public void AddNewMovie(NewMovieVM newMovieVM)
        {
			var newMovie = new Movie()
			{
				Name = newMovieVM.Name,
				Description = newMovieVM.Description,
				StartDate = newMovieVM.StartDate,
				EndDate = newMovieVM.EndDate,
				Price = newMovieVM.Price,
				ImageURL = newMovieVM.ImageURL,
				CinemaId=newMovieVM.CinemaId,
				MovieCategory=newMovieVM.MovieCategory,
				ProducerId=newMovieVM.ProducerId,
			};
			_context.Movies.Add(newMovie);
			_context.SaveChanges();
			foreach(var actorId in newMovieVM.ActorIds)
			{
				var newActorMovie = new Actor_Movie()
				{
					MovieId=newMovie.Id,
					ActorId=actorId
				};
				_context.Actors_Movies.Add(newActorMovie);
				_context.SaveChanges();
			}
        }

        public Movie GetMovieById(int id)
		{
			var movieDetails=_context.Movies
				.Include(c=>c.Cinema)
				.Include(p=>p.Producer)
				.Include(am=>am.Actors_Movies).ThenInclude(a=>a.Actor)
				.FirstOrDefault(n=>n.Id == id);
			return movieDetails;
		}

        public NewMovieDropdownsVM GetNewMovieDropdownsValues()
        {
            var response=new NewMovieDropdownsVM();
			response.Actors=_context.Actors.OrderBy(n=>n.FullName).ToList();
			response.Cinemas=_context.Cinemas.OrderBy(n=>n.Name).ToList();
			response.Producers=_context.Producers.OrderBy(n=>n.FullName).ToList();
			return response;
        }

        public void UpdateMovie(NewMovieVM newMovieVM)
        {
            var dbMovie=_context.Movies.FirstOrDefault(n=>n.Id==newMovieVM.Id);

            if (dbMovie != null)
            {

                dbMovie.Name = newMovieVM.Name;
                dbMovie.Description = newMovieVM.Description;
                dbMovie.StartDate = newMovieVM.StartDate;
                dbMovie.EndDate = newMovieVM.EndDate;
                dbMovie.Price = newMovieVM.Price;
                dbMovie.ImageURL = newMovieVM.ImageURL;
                dbMovie.CinemaId = newMovieVM.CinemaId;
                dbMovie.MovieCategory = newMovieVM.MovieCategory;
                dbMovie.ProducerId = newMovieVM.ProducerId;
                _context.SaveChanges();
            }
            var exitingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == newMovieVM.Id).ToList();
            _context.Actors_Movies.RemoveRange(exitingActorsDb);
            _context.SaveChanges();
         
            foreach (var actorId in newMovieVM.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovieVM.Id,
                    ActorId = actorId
                };
                _context.Actors_Movies.Add(newActorMovie);
                _context.SaveChanges();
            }
        }
    }
}
