using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.ViewModels
{
    public class CollectionViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }

        public string Speed { get; set; }

        public string Endurance { get; set; }
    }
}
