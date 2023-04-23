using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services
{
	public interface IMoviesService:IEntityBaseRepository<Movie>
	{
		Movie GetMovieById(int id);
		NewMovieDropdownsVM GetNewMovieDropdownsValues();
		void AddNewMovie(NewMovieVM newMovieVM);
		void UpdateMovie(NewMovieVM newMovieVM);
	}
}
