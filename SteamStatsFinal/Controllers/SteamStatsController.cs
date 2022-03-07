using Microsoft.AspNetCore.Mvc;
using SteamWebAPI2.Utilities;

using Newtonsoft;
using SteamStatsFinal.Models;

namespace SteamStatsFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SteamStatsController : ControllerBase
    {





        private readonly ILogger<SteamStatsController> _logger;

        public SteamStatsController(ILogger<SteamStatsController> logger)
        {
            _logger = logger;
        }



        [HttpGet("{id}")]
        public FullInfo Get2(ulong id)
        {

            var ui = new UserStats(id);

            IEnumerable<Game> games = ui.populateArray();
            if (games == null)
            {

                ResponseObject responseObject = new ResponseObject();
                responseObject.ErrorMessage = "INVALID USER";
                return (FullInfo)responseObject;
            }

            FullInfo fullInfo = new FullInfo();
            fullInfo.GameList = games;
            fullInfo.userInfo = ui.getUserInfo();
            return fullInfo;
        }


    }
}