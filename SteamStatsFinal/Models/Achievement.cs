namespace SteamStatsFinal.Models
{
    public class Achievement
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ulong? unlockTimestamp { get; set; }
        public bool isFinished { get; set; }

    }
}
