namespace SteamStatsFinal.Models
{
    public class FullInfo : ResponseObject
    {

        public UserInfo userInfo { get; set; }
        public IEnumerable<Game> GameList { get; set; }

    }
}
