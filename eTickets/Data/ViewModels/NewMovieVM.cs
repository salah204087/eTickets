using eTickets.Data;
using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Display(Name ="Movie name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Movie description")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Price in $")]
        [Required]
        public double Price { get; set; }
        [Display(Name = "Movie Poster URL")]
        [Required]
        public string ImageURL { get; set; }
        [Display(Name = "Movie start date")]
        [Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "Movie end date")]
        [Required]
        public DateTime EndDate { get; set; }
        [Display(Name = "Select a category")]
        [Required]
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        [Display(Name = "Select Actor(s)")]
        [Required]
        public List<int> ActorIds{ get; set; }

        //Cinema
        [Display(Name = "Select Cinema")]
        [Required]
        public int CinemaId { get; set; }


        //Producer
        [Display(Name = "Select Producer")]
        [Required]
        public int ProducerId { get; set; }
    }
}
