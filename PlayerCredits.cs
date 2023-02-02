using Spectre.Console;

namespace _21an
{
    internal class playerCurrency
    {
        private CreditsProfiles creditsProfile { get; set; }

        public PlayerStatsDatabase playerStatsDatabase { get; set; }

        public playerCurrency(ref PlayerStatsDatabase playerStatsDataBase)
        {
            playerStatsDatabase = playerStatsDataBase;
            creditsProfile = new CreditsProfiles(ref playerStatsDataBase);
        }

        public int GetUserMoney(string userName)
        {
            var r = playerStatsDatabase.GetOrCreateStats(userName);

            if (r.Matches == 0)
            {
                var newPerson = playerStatsDatabase.GetOrCreateStats(userName);
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