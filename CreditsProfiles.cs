using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace _21an
{
    internal class CreditsProfiles
    {
        private int Credits { get; set; }
        internal object ReturnUserCredits(string nameOfUser)
        {
            PlayerStatsDatabase playerstatsDataBase = new PlayerStatsDatabase();
            var playerStats = playerstatsDataBase.GetPlayerStats(nameOfUser);
            return playerStats.Credits;
        }
    }
}
