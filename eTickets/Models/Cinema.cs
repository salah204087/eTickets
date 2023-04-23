﻿using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Cinema Logo")]
        [Required]

        public string Logo { get; set; }
		[Display(Name = "Cinema Name")]
        [Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50")]
		public string Name { get; set; }
        [Display(Name ="Description")]
        [Required]
        public string Description { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
