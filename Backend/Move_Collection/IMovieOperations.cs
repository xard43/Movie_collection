using Move_Collection.Models;
using Movie_Collection.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie_Collection
{
	public interface IMovieOperations
	{
		public Task<int> AddNewMovie(MovieViewModel model);
		public Task<List<Movie>> GetActivesMovies();
		public Task<int> DeleteMovie(int movieID);
		public Task<Movie> EditMovie(MovieViewModel model, int movieID);
	}
}