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

        [HttpGet("{steamId}")]
        public ResponseObject Get2(ulong steamId)
        {
            var ui = new UserStats(new InfoTemplate(steamId));

            IEnumerable<Game> games = ui.populateArray();

            if (games == null)
            {
                ResponseObject responseObject = new ResponseObject();
                responseObject.ErrorMessage = "INVALID USER";
                return responseObject;
            }

            FullUserInfo fullInfo = new FullUserInfo();
            fullInfo.GameList = games;
            fullInfo.userInfo = ui.getUserInfo();
            return fullInfo;
        }

        [HttpGet("{steamId}/achievements/{appId}")]
        public ResponseObject getAchievements(ulong steamId,ulong appId)
        {
            var ai = new AchievementStats(new InfoTemplate(steamId), appId);
            IEnumerable<Achievement> achievementList = ai.populateAchievementList();

            if (achievementList == null)
            {
                ResponseObject responseObject = new ResponseObject();
                responseObject.ErrorMessage = "INVALID GAME/USER";
                return responseObject;
            }
            FullAchievementInfo achievementInfo = new FullAchievementInfo();
            achievementInfo.AchievementList = achievementList;
            achievementInfo.GameInfo = ai.gameInfo;

            return achievementInfo;
        }
    }
}