public class Program
{
    static bool UserInputIsYes()
    {
        while (true)
        {
            string? userInput = Console.ReadLine()?.ToLower();
            
            if (userInput == "ja")
            {
                return true;
            }
            else if (userInput == "nej")
            {
                return false;
            }
            
        }
    }

    public static bool Play21()
    {
        Console.Clear();
        int userFirstCard = GetRandomCardValue();
        int userSecondCard = GetRandomCardValue();
        int computerFirstCard = GetRandomCardValue();
        int computerSecondCard = GetRandomCardValue();

        int userScore = userFirstCard + userSecondCard;
        int computerScore = computerFirstCard + computerSecondCard;

        if (userFirstCard != userSecondCard)
        {
            Console.WriteLine($"Dina kort är värda {userFirstCard} och {userSecondCard}. Du har {userScore} poäng.");
        }
        else
        {
            Console.WriteLine($"Dina båda kort är värda {userFirstCard} poäng. Totalt har du {userScore} poäng");
        }

        if (computerFirstCard != computerSecondCard)
        {
            Console.WriteLine($"Datorns kort är värda {computerFirstCard} och {computerSecondCard}. Datorn har nu {computerScore} poäng.");
        }
        else
        {
            Console.WriteLine($"Datorns båda kort är värda {computerFirstCard} poäng. Datorn har nu {computerScore} poäng.");
        }
            Console.WriteLine($"Vill du ta ett till kort? (ja/nej)");
        while (UserInputIsYes())
        {
            Console.Clear();
                if (userScore < 21)
                {
                    int newCardValue = GetRandomCardValue();
                    userScore += newCardValue;
                    Console.WriteLine($"Du fick kortet {newCardValue}. Du har nu {userScore} poäng.");
                    if (userScore < 21)
                    {
                        Console.WriteLine($"Datorn har fortfarande {computerScore} poäng.");
                        Console.WriteLine("Vill du ta ett till kort? (ja/nej)");
                    }

                    else if (userScore > 21)
                    {
                        Console.WriteLine($"Otur! Du fick för mycket poäng. Datorn har vunnit med {computerScore} jämfört mot dina {userScore}.");
                        ReturnToMenu();
                        return false;
                    }
                    else if (userScore == 21)
                    {
                        Console.WriteLine("Grattis. Du har vunnit spelet då du fick 21 poäng.");
                        ReturnToMenu();
                        return true;
                    }
                }
            }


        while (computerScore < 21 && computerScore < userScore)
        {
            int noDelay = 1;

            int newPCCardValue = GetRandomCardValue();

            if (computerScore > 21)
            {
                Console.WriteLine($"Du vann! Grattis. Datorn fick {computerScore}, vilket är mer än 21.");
                ReturnToMenu();
                return true;
            }

            else if (computerScore == 21)
            {
                Console.WriteLine($"Datorn plockade upp kortet {newPCCardValue}. Datorn har nu {computerScore} poäng, och vann därför.");
                ReturnToMenu();
                return false;
            }

            else
            {
                computerScore += newPCCardValue;
                Console.WriteLine($"Datorn plockade upp kortet {newPCCardValue}. Datorn har nu {computerScore} poäng.");
            }

            if (noDelay == 1)
            {
                Thread.Sleep(1300);
                noDelay++;
            }
        }

        if (computerScore > 21)
        {
            Console.WriteLine($"Du har vunnit med {userScore} jämfört mot datorn som fick {computerScore}. Grattis!");
            ReturnToMenu();
            return true;
        }

        if (userScore <= 21 && userScore > computerScore)
        {
            Console.WriteLine($"Du har vunnit med {userScore} jämfört mot datorns {computerScore}. Grattis!");
            ReturnToMenu();
            return true;
        }
        else if (computerScore <= 21)
        {
            Console.WriteLine($"Datorn har vunnit med {computerScore} jämfört mot dina {userScore}");
            ReturnToMenu();
            return false;
        }

        else
        {
            return false;
        }
    }

    private static string GetName()
    {
        do
        {
            string? userName;
            Console.Clear();
            Console.WriteLine("");
            Console.Write("Ange ditt namn: ");

            userName = Console.ReadLine();
            bool isAlpha = userName.All(Char.IsLetter);

            if (!isAlpha)
            {
                Console.Clear();    
                Console.WriteLine("");
                Console.WriteLine("Ditt namn får endast innehålla vanliga bokstäver.");
                Console.WriteLine("");
                Console.WriteLine("Tryck vilken tangent som helst för att bekräfta.");
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            if (isAlpha)
            {
                Console.WriteLine("Är du säker? (ja/nej)");
            }

            if (UserInputIsYes())
            {
                return userName;
            }        
        }
        while (true);
    }


    private static int GetRandomCardValue()
    {
        return Random.Shared.Next(1, 10);
    }

    private static void Play21Rules()
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine("Spelet går ut på att man tar kort tills du är så nära 21 som möjligt, men inte mer. Varje kort är värt mellan 1 och 10 poäng.");
        ReturnToMenu();
    }

    private static void ReturnToMenu()
    {
        Console.WriteLine("");
        Console.WriteLine("Tryck på vilken tangent som helst för att gå tillbaka.");
        Console.ReadKey();
        Console.Clear();
    }

    public static void Main()
    {
        PlayerStatsDatabase playerStatsDatabase = new PlayerStatsDatabase();

        string nameOfUser;
        string lastWinner = string.Empty;
        while (true)
        {
            Console.WriteLine("Välkommen till 21an!");
            Console.WriteLine("Välj ett alternativ nedan.");
            Console.WriteLine("");
            Console.WriteLine("1. Spela 21an");
            Console.WriteLine("2. Se senaste vinnaren");
            Console.WriteLine("3. Spelets Regler");
            Console.WriteLine("4. Avsluta spelet");
            string? userInput = Console.ReadLine();



            switch (userInput)
            {
                case "1":
                    nameOfUser = GetName();
                    if (Play21())
                    {
                        playerStatsDatabase.RecordPlayerWin(nameOfUser);
                        lastWinner = nameOfUser;                     
                        Console.Clear();
                    }
                    else
                    {
                        playerStatsDatabase.RecordPlayerLoss(nameOfUser);
                        lastWinner = "Datorn";
                        Console.Clear();
                    }
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine($"Senaste vinnaren var {lastWinner}.");
                    ReturnToMenu();
                    break;
                case "3":
                    Play21Rules();
                    break;
                case "4":
                    return; 
            }
        }
    }
}