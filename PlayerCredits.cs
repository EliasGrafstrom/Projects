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
        PlayerStatsDatabase playerStatsDataBase = new PlayerStatsDatabase();
        public object GetUserMoney(string userName, int userBet)
        {
            var r = playerStatsDataBase.GetPlayerStats(userName);

            if (r == null)
            {
                CreditsProfiles creditsProfile = new CreditsProfiles(userName, 500);
            }
            //creates new user with the name of the user, and the standard credits amount
            foreach ()
            {
                //för varje element i credits-databasen ska namn kollas. när rätt namn har hittats
                //så ska creditsen tas hit, och returneras.
            }
            //Seraches the CreditsProfile class for the right username and uses their credits.
        }

        internal int MakeBet()
        {
            Console.WriteLine($"Hur mycket vill du betta? Du har GetUserMoney credits.");
            int userBet;
            while (!int.TryParse(Console.ReadLine(), out userBet))
            {
                Console.WriteLine("Ange ett heltal.");
            }
            return userBet;
        }
    }
}