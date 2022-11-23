using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21an
{
    internal class playerCurrency
    {
        PlayerStatsDatabase playerStatsDataBase = new PlayerStatsDatabase();
        public int GetUserMoney(string userName, int userBet)
        {
            var r = playerStatsDataBase.GetPlayerStats(userName);

            if (r == null)
            {
                return 500;
            }

        }

        internal int MakeBet()
        {
            Console.WriteLine($"Hur mycket vill du betta? Du har {GetUserMoney} credits.");
            int userBet;
            while (!int.TryParse(Console.ReadLine(), out userBet))
            {
                Console.WriteLine("Ange ett heltal.");
            }
            return userBet;
        }
    }
}