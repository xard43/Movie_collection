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
		[MaxLength(200), Required]
		public string Title { get; set; }
		[Range(1900, 2100, ErrorMessage = "Value  must be between {1} and {2}.")]
		public int? YearOfProduction { get; set; }
		public bool IsDeleted { get; set; }
	}
}
