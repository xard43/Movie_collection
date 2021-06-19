using Microsoft.EntityFrameworkCore;
using Move_Collection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Move_Collection.Database
{
	public class MovieCollectionDbContext : DbContext
	{
		public MovieCollectionDbContext(DbContextOptions<MovieCollectionDbContext> options) : base(options)
		{
			
		}
		public virtual DbSet<Movie> Movie { get; set; }
	}
}
