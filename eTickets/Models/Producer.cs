﻿using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required]
        public string ProfilePictureURL { get; set; }
		[Display(Name = "Full Name")]
        [Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50")]
		public string FullName { get; set; }
		[Display(Name = "Biography")]
        [Required]
		public string Bio { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
