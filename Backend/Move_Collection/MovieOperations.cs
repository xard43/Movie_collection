using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.SecurityTokenService;
using Move_Collection.Controllers;
using Move_Collection.Database;
using Move_Collection.Models;
using Movie_Collection.Database;
using Movie_Collection.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Movie_Collection.Logging;
using System.Threading.Tasks;
using System.Net;

namespace Movie_Collection
{
	public class MovieOperations : IMovieOperations
	{
		private readonly ILogger<MovieController> _logger;
		private readonly MovieCollectionDbContext _db;
		public MovieOperations(MovieCollectionDbContext db, ILogger<MovieController> logger)
		{
			_db = db;
			_logger = logger;
		}
		public async Task<int> AddNewMovie(MovieViewModel model)
		{
			Movie newMovie = new Movie
			{
				Title = model.MovieTitle,
				YearOfProduction = model.MovieYearOfProduction,
				Guid = Guid.NewGuid(),
				IsDeleted = false,
			};

			try
			{
				await _db.Movie.AddAsync(newMovie);
				await _db.SaveChangesAsync();
				string message = "FAIL! Movie was not add to database";
				_logger.LogError(message);
				throw new RequestExceptionMessage(HttpStatusCode.BadRequest, message);
			}
			catch
			{
				_logger.LogError("Movie was add to database succesfully");
			}

			return newMovie.ID;
		}
		public async Task<List<Movie>> GetActivesMovies()
		{
			List<Movie> movies = await _db.Movie.Where(x => x.IsDeleted == false).ToListAsync();
			_logger.LogError("Movies was taken from database succesfully");
			return movies;
		}
		public async Task<int> DeleteMovie(int movieID)
		{
			Movie movie = await MovieDatabaseExtension.GetMovieByIDAsync(_db.Movie, movieID);
			if (movie != null)
			{
				movie.IsDeleted = true;
				_db.Movie.Update(movie);
				_logger.LogError("Movie was removed succesfully");
			}
			else
			{
				string message = "Movie was not found. Movie cannot be updated";
				_logger.LogError(message);
				throw new RequestExceptionMessage(HttpStatusCode.BadRequest, message);
			}
			await _db.SaveChangesAsync();
			return movie.ID;
		}
		public async Task<Movie> EditMovie(MovieViewModel model, int movieID)
		{
			Movie movie = await MovieDatabaseExtension.GetMovieByIDAsync(_db.Movie, movieID);
			if (movie != null)
			{
				if(movie.IsDeleted)
				{
					string message = "Movie is deleted. Movie can not be edited";
					_logger.LogError(message);
					throw new RequestExceptionMessage(HttpStatusCode.BadRequest, message);
				}
				else if (model.MovieTitle != movie.Title || model.MovieYearOfProduction != movie.YearOfProduction)
				{
					movie.Title = model.MovieTitle;
					movie.YearOfProduction = model.MovieYearOfProduction;
					_db.Movie.Update(movie);
				}
				else
				{
					_logger.LogError("No changes. Movie was not edited.");
				}
			}
			else
			{
				string message = "Movie was not found. Movie cannot be removed";
				_logger.LogError(message);
				throw new RequestExceptionMessage(HttpStatusCode.BadRequest, message);
			}
			await _db.SaveChangesAsync();
			return movie;
		}
	}
}
