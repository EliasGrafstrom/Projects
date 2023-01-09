using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public int GetUserMoney(string userName)
        {
            var r = playerStatsDataBase.GetPlayerStats(userName);

            if (r.Matches == 0)
            {
                var newPerson = playerStatsDataBase.GetPlayerStats(userName);
                int result =  newPerson.Credits = 500;
                return result;
            }
            //if person doesn't exist, create person and give them credits.
            return creditsProfile.ReturnUserCredits(userName);
        }

        internal int MakeBet(int userMoney)
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"Hur mycket vill du betta? Du har [cadetblue]{userMoney}[/] credits.");

                int userBet;
                while (!int.TryParse(Console.ReadLine(), out userBet))
                {
                    Console.WriteLine("Ange ett heltal.");
                }
                while (userBet <= userMoney)
                {
                    return userBet;
                }
                Console.WriteLine("Du kan inte betta mer än vad du har. Tryck på vilken tangent som helst för att bekräfta.");
                Console.ReadKey();
            } 
        }
    }
}