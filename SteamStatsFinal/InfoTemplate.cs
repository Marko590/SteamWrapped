using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;

namespace SteamStatsFinal
{
    public class InfoTemplate
    {
        public ulong steamId { get; set; }
        public string url { get; set; }
        private SteamWebInterfaceFactory webInterfaceFactory { get; set; }
        private SteamStore steamInterface { get; set; }


        public InfoTemplate(ulong steamId)
        {
            this.url = "http://steamcommunity.com/profiles/" + steamId + "/games?tab=all&xml=1";
            this.steamId = steamId;
            this.webInterfaceFactory = new SteamWebInterfaceFactory(System.Environment.GetEnvironmentVariable("SteamSecret"));
            this.steamInterface = webInterfaceFactory.CreateSteamStoreInterface();
            this.url = url;
        }

        public T createInterface<T>() => (T)webInterfaceFactory.CreateSteamWebInterface<T>();
    }
}
