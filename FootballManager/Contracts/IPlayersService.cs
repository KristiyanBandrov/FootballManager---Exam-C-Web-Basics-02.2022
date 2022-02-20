using FootballManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Contracts
{
    public interface IPlayersService
    {
        (bool created, string error) Add(AddViewModel model);

        IEnumerable<CollectionViewModel> GetAllPlayers();

        public void AddPlayerToUser(string playerId, string id);
        void RemoveFromCollection(string playerId);
        IEnumerable<CollectionViewModel> GetPlayersToUser(string userId);
        bool CheckPlayerByUser(string playerId, string id);
    }
}
