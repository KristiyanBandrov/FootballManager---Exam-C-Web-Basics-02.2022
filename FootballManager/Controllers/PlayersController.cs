using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using FootballManager.Contracts;
using FootballManager.ViewModels;

namespace FootballManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IUserService userService;
        private readonly IPlayersService playersService;

        public PlayersController(Request request,
            IUserService _userService,
            IPlayersService _playerService) 
            : base(request)
        {
            userService = _userService;
            playersService = _playerService;
        }

        [Authorize]
        public Response All()
        {
            string username = userService.GetUsername(User.Id);

            var model = new
            {
                Username = username,
                IsAuthenticated = true,
                Plaeyers = playersService.GetAllPlayers()
            };

            return View(model, "/Players/All");
        }

        [Authorize]
        public Response Add()
        {
            return View(new { IsAuthenticated = true });
        }

        [HttpPost]
        [Authorize]
        public Response Add(AddViewModel model)
        {
            var (created, error) = playersService.Add(model);

            if (!created)
            {
                return Redirect("/Players/Add"); ;
            }

            return Redirect("/");
        }

        [Authorize]
        public Response AddToCollection(string playerId)
        {
            bool check = playersService.CheckPlayerByUser(playerId, User.Id);

            if (check)
            {
                return Redirect("/Players/All");
            }

            playersService.AddPlayerToUser(playerId, User.Id);

            return Redirect("/");
        }

        [Authorize]
        public Response Collection()
        {
            IEnumerable<CollectionViewModel> players = playersService.GetPlayersToUser(User.Id);

            return View(new
            {
                players = players,
                IsAuthenticated = true
            }, "/Players/Collection");
        }

        [Authorize]
        public Response RemoveFromCollection(string playerId)
        {
            playersService.RemoveFromCollection(playerId);

            return Redirect("/Players/Collection");
        }
    }
}
