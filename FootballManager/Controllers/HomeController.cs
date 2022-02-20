namespace FootballManager.Controllers
{
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
    using FootballManager.Contracts;
    using FootballManager.Services;

    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IPlayersService playersService;

        public HomeController(Request request, 
            IUserService _userService,
            IPlayersService _playerService)
            : base(request)
        {
            userService = _userService;
            playersService = _playerService;
        }

        public Response Index()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("Players/All");
            }

            return this.View(new { IsAuthenticated = false });
        }


    }
}
