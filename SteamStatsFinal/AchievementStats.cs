using SteamStatsFinal.Models;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace SteamStatsFinal
{
    public class AchievementStats
    {
        public Game gameInfo { get; set; }
        private InfoTemplate infoTemplate;
        public ulong appId;

        public AchievementStats(InfoTemplate infoTemplate,ulong appId)
        {
            this.infoTemplate= infoTemplate;
            this.appId = appId;
        }

        public List<XElement>? fetchAchievementList()
        {
            string achievementsUrl = "https://steamcommunity.com/profiles/" + infoTemplate.steamId + "/stats/" + appId + "/?tab=achievements&xml=1";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] data;
            using (WebClient webClient = new WebClient())
                data = webClient.DownloadData(achievementsUrl);

            string str = Encoding.GetEncoding("Windows-1252").GetString(data);
            XDocument xd = new XDocument();
            try
            {
                xd = XDocument.Parse(str);
            }
            catch (Exception ex)
            {
                return null;
            }

            if (xd.Element("response") != null)
            {
                return null;
            }

            Game gameInfo = new Game();

            gameInfo.Name = xd.Element("playerstats").Element("game").Element("gameName").Value;

            gameInfo.appId = ulong.Parse(xd.Element("playerstats").Element("game").Element("gameFriendlyName").Value);

            string hoursPlayed = xd.Element("playerstats").Element("stats").Element("hoursPlayed")?.Value ?? "0";
            hoursPlayed = hoursPlayed.Replace(",", String.Empty);
            gameInfo.Hours = float.Parse(hoursPlayed);

            var achievementList = xd.Element("playerstats").Element("achievements").Elements().ToList();
            return achievementList;
        }

        public IEnumerable<Achievement>? populateAchievementList()
        {
            var allAchievements = fetchAchievementList();
            if(allAchievements == null)
                {
                    return null;
                }

            List<Achievement> achievements=new List<Achievement>();

            foreach(var achievement in allAchievements)
            {
                Achievement current = new Achievement();

                current.Name = achievement.Element("name").Value;
                current.Description=achievement.Element("description").Value;
                current.unlockTimestamp = ulong.Parse(achievement.Element("unlockTimestamp")?.Value ?? "0");
                current.isFinished = int.Parse(achievement.FirstAttribute.Value) == 1 ? true : false;

                achievements.Add(current);
            }
            return achievements;
        }
    }
}
