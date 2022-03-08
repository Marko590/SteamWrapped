using SteamWebAPI2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using SteamStatsFinal.Models;
using SteamWebAPI2.Utilities;
using SteamStatsFinal.Models;

namespace SteamStatsFinal
{


    internal class UserStats { 

        private InfoTemplate infoTemplate;

        public UserStats(InfoTemplate info) 
        {
            this.infoTemplate = info;
        }
        public List<XElement>? getGameList()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] data;
            using (WebClient webClient = new WebClient())
                data = webClient.DownloadData(infoTemplate.url);

            string str = Encoding.GetEncoding("Windows-1252").GetString(data);
            XDocument xd = XDocument.Parse(str);
            try
            {
                var gamesList = xd.Element("gamesList")?.Element("games").Elements().ToList() ?? null;
                return gamesList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Tuple<string, float, ulong>>? getHoursPerGame()
        {
            List<Tuple<string, float, ulong>> list = new List<Tuple<string, float, ulong>>();
            var gameList = getGameList();
            if (gameList == null)
            {
                return null;
            }

            foreach (var game in gameList)
            {
                string hoursPlayed = game.Element("hoursOnRecord")?.Value ?? "0";
                hoursPlayed = hoursPlayed.Replace(",", String.Empty);

                var name = game.Element("name").Value;

                var appId = game.Element("appID").Value;

                list.Add(new Tuple<string, float, ulong>(name, float.Parse(hoursPlayed), ulong.Parse(appId)));
            }
            return list;
        }

        public IEnumerable<Game>? populateArray()
        {

            List<Tuple<string, float, ulong>> list = getHoursPerGame();
            if (list == null)
            {
                return null;
            }
            List<Game> games = new List<Game>();

            foreach (var game in list)
            {
                Game current = new Game();
                current.Name = game.Item1;
                current.Hours = game.Item2;
                current.appId = game.Item3;
                games.Add(current);
            }
            return games;
        }

        public UserInfo getUserInfo()
        {
            var userInterface = infoTemplate.createInterface<SteamWebAPI2.Interfaces.SteamUser>();
            var summaryResponse = userInterface.GetPlayerSummaryAsync(infoTemplate.steamId).Result;
            var summaryData = summaryResponse.Data;

            UserInfo userInfo = new UserInfo();

            userInfo.AvatarUrl = summaryData.AvatarUrl;
            userInfo.LastOnline = summaryData.LastLoggedOffDate;
            userInfo.UserName = summaryData.Nickname;

            return userInfo;
        }
    }
}
