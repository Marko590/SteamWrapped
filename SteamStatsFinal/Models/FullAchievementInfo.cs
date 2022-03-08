namespace SteamStatsFinal.Models
{
    public class FullAchievementInfo : ResponseObject
    {
        public Game GameInfo { get; set; }
        public IEnumerable<Achievement> AchievementList{ get; set; }
    }
}
