using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _21an
{
    internal class playerCurrency
    {
        CreditsProfiles creditsProfile = new CreditsProfiles();
        PlayerStatsDatabase playerStatsDataBase = new PlayerStatsDatabase();
        public object GetUserMoney(string userName)
        {
            var r = playerStatsDataBase.GetPlayerStats(userName);

            if (r == null)
            {
                //creates new user with the name of the user, and the standard credits amount
            }
            return creditsProfile.ReturnUserCredits(userName);
        }

        internal int MakeBet(string userName)
        {
            Console.WriteLine($"Hur mycket vill du betta? Du har {creditsProfile.ReturnUserCredits(userName)} credits.");
            int userBet;
            while (!int.TryParse(Console.ReadLine(), out userBet))
            {
                Console.WriteLine("Ange ett heltal.");
            }
            return userBet;
        }
    }
}