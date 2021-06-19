using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Move_Collection.Database;
using Move_Collection.Models;
using Movie_Collection;
using Movie_Collection.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Move_Collection.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MovieController : ControllerBase
	{
		private readonly MovieCollectionDbContext _db;
		private readonly IMovieOperations _movieOperations;
		private readonly ILogger<MovieController> _logger;

		public MovieController(ILogger<MovieController> logger, MovieCollectionDbContext db, IMovieOperations movieOperation)
		{
			_db = db;
			_logger = logger;
			_movieOperations = movieOperation;
		}

		[HttpGet]
		[Route("GetMovies")]
		public async Task<IActionResult> GetAllMovies()
		{
			List<Movie> movies = await _movieOperations.GetActivesMovies();
			return new JsonResult(movies);
		}

		[HttpPost]
		[Route("AddMovie")]
		public async Task<IActionResult> AddNewMovie([FromBody] MovieViewModel model)
		{
			int newMovieID = await _movieOperations.AddNewMovie(model);
			
			return Ok(new { Id = newMovieID });
		}
		[HttpPost]
		[Route("DeleteMovie")]
		public async Task<IActionResult> DeleteMovie(int movieID)
		{
			int removedMovie = await _movieOperations.DeleteMovie(movieID);
			return Ok(new { Id = removedMovie });
		}
		[HttpPost]
		[Route("EditMovie")]
		public async Task<IActionResult> EditMovie([FromBody] MovieViewModel model, int movieID)
		{
			Movie editedMovie = await _movieOperations.EditMovie(model,movieID);
			return Ok(editedMovie);
		}
	}
}
