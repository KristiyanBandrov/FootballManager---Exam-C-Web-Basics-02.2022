using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Data.Models
{
    public class Player
    {
        public Player()
        {
            UserPlayers = new List<UserPlayer>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(80)]
        [Required]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(20)]
        public string Position { get; set; }

        [Required]
        [MaxLength(10)]
        public byte Speed { get; set; }

        [Required]
        [MaxLength(10)]
        public byte Endurance { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public ICollection<UserPlayer> UserPlayers { get; set; }
    }
}
