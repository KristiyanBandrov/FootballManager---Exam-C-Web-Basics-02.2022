using FootballManager.Contracts;
using FootballManager.Data.Common;
using FootballManager.Data.Models;
using FootballManager.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Services
{
    internal class PlayerService : IPlayersService
    {
        private readonly IRepository repo;
        private readonly IValidationService validationService;

        public PlayerService(IRepository _repo, 
            IValidationService _validationService)
        {
            repo = _repo;
            validationService = _validationService;
        }

        public (bool created, string error) Add(AddViewModel model)
        {
            bool created = false;

            string error = null;

            var (isValid, validationError) = validationService.ValidateModel(model);

            if (!isValid)
            {
                return (isValid, validationError);
            }

            byte speed;

            byte endurance;

            if (!byte.TryParse(model.Speed, NumberStyles.Float, CultureInfo.InvariantCulture, out speed)
                || speed >= 10)
            {
                return (false, "Speed must be equal or less than 10");
            }

            if (!byte.TryParse(model.Endurance, NumberStyles.Float, CultureInfo.InvariantCulture, out endurance)
                || endurance >= 10)
            {
                return (false, "Endurance must be equal or less than 10");
            }

            var player = new Player()
            {
                FullName = model.FullName,
                Description = model.Description,
                Speed = speed,
                Endurance = endurance,
                ImageUrl = model.ImageUrl,
                Position = model.Position,
            };

            try
            {
                repo.Add(player);
                repo.SaveChanges();
                created = true;
            }
            catch (Exception)
            {
                error = "Could not save player.";
            }

            return (created, error);
        }

        public void AddPlayerToUser(string playerId, string id)
        {
            var user = repo.All<User>()
                .Where(u => u.Id == id)
                .FirstOrDefault();

            var realPlayerId = int.Parse(playerId);

            var player = repo.All<Player>()
                .FirstOrDefault(p => p.Id == realPlayerId);

            user.UserPlayers.Add(new UserPlayer() { PlayerId = player.Id, UserId = user.Id});

            try
            {
                repo.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        public bool CheckPlayerByUser(string playerId, string id)
        {
            return repo.All<UserPlayer>()
                .Any(u => u.UserId == id && u.User.UserPlayers.Any(p => p.PlayerId == int.Parse(playerId)));
        }

        public IEnumerable<CollectionViewModel> GetAllPlayers()
        {
            return repo.All<Player>()
                .Select(p => new CollectionViewModel()
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    Description = p.Description,
                    Endurance = p.Endurance.ToString(),
                    Speed = p.Speed.ToString(),
                    ImageUrl = p.ImageUrl,
                    Position = p.Position
                })
                .ToList();
            
        }

        public IEnumerable<CollectionViewModel> GetPlayersToUser(string userId)
        {
            return repo.All<UserPlayer>()
                .Where(up => up.UserId == userId)
                .Select(p => new CollectionViewModel()
                {
                    Id = p.PlayerId,
                    FullName = p.Player.FullName,
                    Description = p.Player.Description,
                    Endurance = p.Player.Endurance.ToString(),
                    Speed = p.Player.Speed.ToString(),
                    ImageUrl = p.Player.ImageUrl,
                    Position = p.Player.Position
                })
                .ToList();
        }

        public void RemoveFromCollection(string playerId)
        {
            var realPlayerId = int.Parse(playerId);

            var playerToRemove = repo.All<UserPlayer>()
                .Where(p => p.PlayerId == realPlayerId)
                .FirstOrDefault();

            repo.Remove<UserPlayer>(playerToRemove);

            try
            {
                repo.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

    }
}
