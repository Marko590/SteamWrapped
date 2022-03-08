namespace SteamStatsFinal.Models
{
    public class FullUserInfo : ResponseObject
    {
        public UserInfo userInfo { get; set; }
        public IEnumerable<Game> GameList { get; set; }
    }
}
