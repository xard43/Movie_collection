using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Move_Collection.Models
{
	public class Movie
	{
		[Key]
		public int ID { get; set; }
		public Guid Guid { get; set; }
		[MaxLength(150), Required]
		public string Title { get; set; }
		[MinLength(1900), MaxLength(2100)]
		public int YearOfProduction { get; set; }
	}
}
