using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Data.Models
{
    public class User
    {
        public User()
        {
            UserPlayers = new List<UserPlayer>();
        }

        [Key]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(60)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; } // trqbva da se heshira i da e max 20 predi hesha

        public ICollection<UserPlayer> UserPlayers { get; set; } = new List<UserPlayer>();
    }
}
