using _21an;
using Spectre.Console;
using static _21an.Cards;

public class Program
{
    public Card? Card { get; set; }
    public Card? SecondCard { get; set; }
    public int Value { get; set; }
    public int SecondValue { get; set; }
    public int Score { get; set; }

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
    static int[] DrawCardsAndAsk(int userFirstValue, int userSecondValue, int computerFirstValue, int computerSecondValue, int userScore, int computerScore, Card userCard, Card userSecondCard, Card computerCard, Card computerSecondCard)
    {
        userScore = userFirstValue + userSecondValue;
        AnsiConsole.MarkupLine($"Du fick korten [chartreuse3_1]{userCard}[/] och [chartreuse3_1]{userSecondCard}[/]. Du har [chartreuse3_1]{userScore}[/] poäng.");
        computerScore = computerFirstValue + computerSecondValue;
        AnsiConsole.MarkupLine($"Datorn fick korten [chartreuse3_1]{computerCard}[/] och [chartreuse3_1]{computerSecondCard}[/]. Datorn har nu [chartreuse3_1]{computerScore}[/] poäng.");
  
        AnsiConsole.MarkupLine($"Vill du ta ett till kort? [mediumspringgreen](ja/nej)[/]");
        int[] scores = { userScore, computerScore };
        return scores;
    }



    public static void AskForCard(int userScore, int computerScore)
    {
        if (userScore < 21)
        {
            AnsiConsole.MarkupLine($"Datorn har fortfarande [chartreuse3_1]{computerScore}[/] poäng.");
            AnsiConsole.MarkupLine("Vill du ta ett till kort? [mediumspringgreen](ja/nej)[/]");
        }
    }
    public static bool UserScoreAbove21(int userScore, int computerScore)
    {
        if (userScore > 21)
        {
            AnsiConsole.MarkupLine($"Otur! Du fick för mycket poäng. Datorn har vunnit med [chartreuse3_1]{computerScore}[/] jämfört mot dina [chartreuse3_1]{userScore}[/].");
            ReturnToMenu();
            return false;
        }
        else return true;
    }

    public static bool GiveNewCard(ref Deck deck, ref Program user, ref Program computer)
    {
        var newUserCard = deck.Draw();
        Console.Clear();
        if (user.Score < 21)
        {
            int newUserCardValue = (int)newUserCard.Value;
            user.Score += newUserCardValue;
            AnsiConsole.MarkupLine($"Du fick kortet [chartreuse3_1]{newUserCard}[/]. Du har nu [chartreuse3_1]{user.Score}[/] poäng.");
            return true;
        }
        else return false;
    }

    public static bool UserScoreIs21(int userScore)
    {
        if (userScore == 21)
        {
            AnsiConsole.MarkupLine("Grattis. Du har vunnit spelet då du fick [chartreuse3_1]21[/] poäng.");
            ReturnToMenu();
            return true;
        }
        else return false;
    }

    public static object[] DeclareCards(ref Deck deck)
    {
        Program user = new Program();
        Program computer = new Program();

        user.Card = deck.Draw();
        user.SecondCard = deck.Draw();
        computer.Card = deck.Draw();
        computer.SecondCard = deck.Draw();

        user.Value = (int)user.Card.Value;
        user.SecondValue = (int)user.SecondCard.Value;
        computer.Value = (int)computer.Card.Value;
        computer.SecondValue = (int)computer.SecondCard.Value;

        int userScore = user.Value + user.SecondValue;
        int computerScore = computer.Value + computer.SecondValue;

        Program[] objects = {user, computer};
        return objects;
    }

    public static bool Play21()
    {
        Console.Clear();
        Deck deck = new Deck();

        Program[] objects = (Program[])DeclareCards(ref deck);
        var user = objects[0];
        var computer = objects[1];

        int[] scores = DrawCardsAndAsk(user.Value, user.SecondValue, computer.Value, computer.SecondValue, user.Score, computer.Score, user.Card, user.SecondCard, computer.Card, computer.SecondCard);
        user.Score = scores[0];
        computer.Score = scores[1];

        while (UserInputIsYes())
        {
            if (!GiveNewCard(ref deck, ref user, ref computer))
            {
                return true;
            }
            AskForCard(user.Score, computer.Score);
        }

        while (computer.Score < 21 && computer.Score < user.Score)
        {
            int noDelay = 1;

            Card newPCCard = deck.Draw();
            int newPCCardValue = (int)newPCCard.Value;

            if (computer.Score > 21)
            {
                AnsiConsole.MarkupLine($"Du vann! Grattis. Datorn fick [chartreuse3_1]{computer.Value}[/], vilket är mer än [chartreuse3_1]21[/].");
                ReturnToMenu();
                return true;
            }

            else if (computer.Score == 21)
            {
                AnsiConsole.MarkupLine($"Datorn plockade upp kortet [chartreuse3_1]{newPCCard}[/]. Datorn har nu [chartreuse3_1]21[/] poäng, och vann därför.");
                ReturnToMenu();
                return false;
            }
            else
            {
                computer.Score += newPCCardValue;
                AnsiConsole.MarkupLine($"Datorn plockade upp kortet [chartreuse3_1]{newPCCard}[/]. Datorn har nu [chartreuse3_1]{computer.Score}[/] poäng.");
            }
            if (noDelay == 1)
            {
                Thread.Sleep(1300);
                noDelay++;
            }
        }

        if (computer.Score > 21)
        {
            AnsiConsole.MarkupLine($"Du har vunnit med [chartreuse3_1]{user.Score}[/] jämfört mot datorn som fick [chartreuse3_1]{computer.Score}[/]. Grattis!");
            ReturnToMenu();
            return true;
        }

        if (user.Score <= 21 && user.Score > computer.Score)
        {
            AnsiConsole.MarkupLine($"Du har vunnit med [chartreuse3_1]{user.Score}[/] jämfört mot datorns [chartreuse3_1]{computer.Score}[/]. Grattis!");
            ReturnToMenu();
            return true;
        }
        else if (computer.Score <= 21)
        {
            AnsiConsole.MarkupLine($"Datorn har vunnit med [chartreuse3_1]{computer.Score}[/] jämfört mot dina [chartreuse3_1]{user.Score}[/].");
            ReturnToMenu();
            return false;
        }
        else
        {
            return false;
        }
    }
    private static string InputName()
    {
        while (true)
        {
            string? userName = String.Empty;
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
    }

    private static void Menu(string userInput, string nameOfUser, string lastWinner, ref PlayerStatsDatabase playerStats)
    {
        switch (userInput)
        {
            case "1":
                nameOfUser = InputName();
                playerCurrency currency = new playerCurrency();
                int userMoney = currency.GetUserMoney(nameOfUser);
                int userBet = currency.MakeBet(userMoney);

                if (Play21())
                {
                    playerStats.RecordPlayerWin(nameOfUser, userMoney, userBet);
                    lastWinner = nameOfUser;
                    Console.Clear();
                }
                else
                {
                    playerStats.RecordPlayerLoss(nameOfUser, userMoney, userBet);
                    lastWinner = "Datorn";
                    Console.Clear();
                }
                break;
            case "2":
                Console.Clear();
                Console.WriteLine("");
                AnsiConsole.MarkupLine($"Senaste vinnaren var [darkgoldenrod]{lastWinner}[/].");
                ReturnToMenu();
                break;
            case "3":
                Play21Rules();
                break;
            case "4":
                string userName = SearchStatistics().FirstCharToUpper();
                var statisticsResult = playerStats.GetPlayerStats(userName);
                Console.WriteLine($"Spelare: {userName} {statisticsResult}");
                ReturnToMenu();
                break;
            case "5":
                Console.Clear();
                foreach (var user in playerStats.GetAllStats())
                {
                    Console.WriteLine($"{user.Name.FirstCharToUpper()} {user.Stats}\n");
                }
                ReturnToMenu();
                break;
            case "6":
                Environment.Exit(0);
                break;
        }
    }

    private static void Play21Rules()
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine("Spelet går ut på att man tar kort tills du har 21, eller så nära som möjligt, men inte mer.");
        Console.WriteLine("Ess är värt 1 poäng, knekt 11, dam 12 och kung 13.");
        Console.WriteLine("Det du bettar dubblas om du vinner, om du förlorar så förlorar du det som du bettat.");
        Console.WriteLine("Du får 10 poäng för varje match du förlorar, så du inte fastnar på 0 poäng.");
        ReturnToMenu();
    }

    internal static void ReturnToMenu()
    {
        Console.WriteLine("");
        AnsiConsole.MarkupLine("[darkgoldenrod]   Tryck på vilken tangent som helst för att gå tillbaka.[/]");
        Console.ReadKey();
        
        Console.Clear();
    }
    private static string SearchStatistics()
    {
        Console.Clear();
        Console.Write("Skriv namnet du vill se statistik på: ");
        string? inputName = Console.ReadLine().ToLower();
        return inputName;
    }

    public static void Main()
    {
        PlayerStatsDatabase playerStats = new PlayerStatsDatabase();
        string lastWinner = String.Empty;
        string nameOfUser = String.Empty;
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[dodgerblue1]  Välkommen till[/] [deepskyblue2]21an![/]");
            Console.WriteLine("  Välj ett alternativ nedan.");
            Console.WriteLine("");
            AnsiConsole.MarkupLine("[darkgoldenrod]  1.[/] Spela 21an");
            AnsiConsole.MarkupLine("[darkgoldenrod]  2.[/] Senaste Vinnaren");
            AnsiConsole.MarkupLine("[darkgoldenrod]  3.[/] Spelets Regler");
            AnsiConsole.MarkupLine("[darkgoldenrod]  4.[/] Sök Statistiken");
            AnsiConsole.MarkupLine("[darkgoldenrod]  5.[/] Visa All Statistik");
            AnsiConsole.MarkupLine("[darkgoldenrod]  6.[/] Avsluta Spelet");
            string? userInput = Console.ReadLine();

            Menu(userInput, nameOfUser, lastWinner, ref playerStats);
        }
    }
}

