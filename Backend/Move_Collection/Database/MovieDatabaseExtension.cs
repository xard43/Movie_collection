using Microsoft.EntityFrameworkCore;
using Move_Collection.Database;
using Move_Collection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Collection.Database
{
	public static class MovieDatabaseExtension
	{
		public static async Task<Movie> GetMovieByIDAsync(DbSet<Movie> source, int movieID)
		{
			Movie movie = await source.FindAsync(movieID);
			return movie;
		}
	}
}
