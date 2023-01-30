using System;
namespace _21an
{
    internal class CreditsProfiles
    {
        private int Credits { get; set; }
        internal int ReturnUserCredits(string nameOfUser)
        {
            PlayerStatsDatabase playerstatsDataBase = new PlayerStatsDatabase();
            var playerStats = playerstatsDataBase.GetPlayerStats(nameOfUser);
            return playerStats.Credits;
        }
    }
}
