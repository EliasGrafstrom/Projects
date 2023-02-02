using System;
namespace _21an
{
    internal class CreditsProfiles
    {
        public PlayerStatsDatabase playerStatsDatabase { get; set; }

        public CreditsProfiles(ref PlayerStatsDatabase playerStatsDataBase)
        {
            playerStatsDatabase = playerStatsDataBase;
        }

        private int Credits { get; set; }
        internal int ReturnUserCredits(string nameOfUser)
        {
            var playerStats = playerStatsDatabase.GetOrCreateStats(nameOfUser);
            return playerStats.Credits;
        }
    }
}
