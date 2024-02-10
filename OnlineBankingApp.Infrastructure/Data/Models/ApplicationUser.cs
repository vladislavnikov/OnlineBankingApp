using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineBankingApp.Infrastructure.Data.Models
{
	public class ApplicationUser : IdentityUser
	{
		public ApplicationUser()
		{
			Cards = new List<Card>();
		}

		[Required]
		[StringLength(20)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(20)]
		public string LastName { get; set; }

		public ICollection<Card> Cards { get; set; }
	}
}
