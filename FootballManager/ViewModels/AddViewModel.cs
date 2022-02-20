using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.ViewModels
{
    public class AddViewModel
    {
        [Required]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string FullName { get; set; }

        //[Required]
        //[StringLength(200,  ErrorMessage = "{0} must be less than {1} characters")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Position { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "{0} must be less than {1}")]
        public string Speed { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "{0} must be less than {1}")]
        public string Endurance { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "{0} must be less than {1} characters")]
        public string Description { get; set; }
    }
}
