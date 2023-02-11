using _21an;
using Spectre.Console;
public class Program
{
    public Card? Card { get; set; }
    public Card? SecondCard { get; set; }
    public int Value { get; set; }
    public int SecondValue { get; set; }
    public int Score { get; set; }


    private static bool CheckForWin(ref Program user, ref Program computer)
    {
        if (user.Score <= 21 && user.Score > computer.Score)
        {
            AnsiConsole.MarkupLine($"Du har vunnit med [cadetblue]{user.Score}[/] jämfört mot datorns [cadetblue]{computer.Score}[/]. Grattis!");
            ReturnToMenu();
            return true;
        }
        else if (computer.Score <= 21)
        {
            AnsiConsole.MarkupLine($"Datorn har vunnit med [cadetblue]{computer.Score}[/] jämfört mot dina [cadetblue]{user.Score}[/].");
            ReturnToMenu();
            return false;
        }
        else
        {
            return false;
        }
    }
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
        AnsiConsole.MarkupLine($"Du fick korten [chartreuse4]{userCard}[/] och [chartreuse4]{userSecondCard}[/]. Du har [cadetblue]{userScore}[/] poäng.");
        computerScore = computerFirstValue + computerSecondValue;
        AnsiConsole.MarkupLine($"Datorn fick korten [chartreuse4]{computerCard}[/] och [chartreuse4]{computerSecondCard}[/]. Datorn har nu [cadetblue]{computerScore}[/] poäng.");
  
        AnsiConsole.MarkupLine($"Vill du ta ett till kort? [royalblue1](ja/nej)[/]");
        int[] scores = { userScore, computerScore };
        return scores;
    }

    public static void AskForCard(Card userCard, int userScore, int computerScore)
    {
            AnsiConsole.MarkupLine($"Du fick kortet [chartreuse4]{userCard}[/]. Du har nu [cadetblue]{userScore}[/] poäng.");
            AnsiConsole.MarkupLine($"Datorn har fortfarande [cadetblue]{computerScore}[/] poäng.");
            AnsiConsole.MarkupLine("Vill du ta ett till kort? [royalblue1](ja/nej)[/]");
    }

    public static int CardValue(int cardValue) => cardValue > 10 ? 10 : cardValue;
    //returns 10 if the card is above 10, if it's under 10 it returns the original value

    public static bool GiveNewCard(ref Deck deck, ref Program user, ref Program computer)
    {
        var newUserCard = deck.Draw();
        Console.Clear();
        if (user.Score < 21)
        {
            int newUserCardValue = CardValue((int)newUserCard.Value);
            user.Score += newUserCardValue;
            user.Card = newUserCard;
            return true;
        }
        else return false;
    }

    private static int CheckWinOrDraw(ref Program user, ref Program computer, ref Card newPCCard)
    {
        if (computer.Score > 21)
        {
            AnsiConsole.MarkupLine($"Du vann! Grattis. Datorn fick [cadetblue]{computer.Value}[/], vilket är mer än [cadetblue]21[/].");
            ReturnToMenu();
            return 1;
        }

        else if (computer.Score == 21)
        {
            AnsiConsole.MarkupLine($"Datorn plockade upp kortet [chartreuse4]{newPCCard}[/]. Datorn har nu [cadetblue]21[/] poäng, och vann därför.");
            ReturnToMenu();
            return 2;
        }
        else return 3;
    }

    public static object[] DeclareCards(ref Deck deck)
    {
        Program user = new Program();
        Program computer = new Program();

        user.Card = deck.Draw();
        user.SecondCard = deck.Draw();
        computer.Card = deck.Draw();
        computer.SecondCard = deck.Draw();

        user.Value = CardValue((int)user.Card.Value);
        user.SecondValue = CardValue((int)user.SecondCard.Value);
        computer.Value = CardValue((int)computer.Card.Value);
        computer.SecondValue = CardValue((int)computer.SecondCard.Value);

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
            Console.Clear();
            if (GiveNewCard(ref deck, ref user, ref computer))
            {
                if (user.Score < 21)
                {
                    AskForCard(user.Card, user.Score, computer.Score);
                }
                else if (!CheckForWin(user.Score, ref user))
                {
                    userLost(user.Score);
                    return false;
                    //user lost
                }
                else
                {
                    userWon();
                    return true;
                    //user win
                }
            }
            else if (!GiveNewCard(ref deck, ref user, ref computer))
            {
                AnsiConsole.MarkupLine($"Du fick kortet [chartreuse4]{user.Card}[/]. Du har nu [cadetblue]{user.Score}[/] poäng");
                userLost(user.Score);
                return false;
            }
         }

        while (computer.Score < 21 && computer.Score < user.Score)
        {
            int noDelay = 1;
            Console.Clear();
            Card newPCCard = deck.Draw();
            computer.Value = CardValue((int)newPCCard.Value);

            int winResult = CheckWinOrDraw(ref user, ref computer, ref newPCCard);

            if (winResult == 1) return true; else if (winResult == 2) return false;
            else
            {
                computer.Score += computer.Value;
                AnsiConsole.MarkupLine($"Datorn plockade upp kortet [chartreuse4]{newPCCard}[/]. Datorn har nu [cadetblue]{computer.Score}[/] poäng.");
            }
            if (noDelay == 1)
            {
                Thread.Sleep(1300);
                noDelay++;
            }
        }

        if (computer.Score > 21)
        {
            AnsiConsole.MarkupLine($"Du har vunnit med [cadetblue]{user.Score}[/] jämfört mot datorn som fick [cadetblue]{computer.Score}[/]. Grattis!");
            ReturnToMenu();
            return true;
        }
        var secondWinResult = (bool)CheckForWin(ref user, ref computer);
        if (secondWinResult) return true; else return false;
    }

    private static bool userLost(int score)
    {
        AnsiConsole.MarkupLine($"Du har förlorat då du fick [cadetblue]{score}[/] poäng, som är mer än [cadetblue]21[/].");
        ReturnToMenu(); 
        return false;
    }

    private static bool userWon()
    {
        AnsiConsole.MarkupLine($"Du har vunnit med [cadetblue]21[/] poäng. Grattis!");
        ReturnToMenu();
        return true;
    }

    private static bool CheckForWin(int score, ref Program user)
    {
        AnsiConsole.MarkupLine($"Du fick kortet [cadetblue]{user.Card}[/]. Du har nu [cadetblue]{user.Score}[/] poäng.");
        if (score > 21)
        {
            return false;
            //score is always 21 or over here
        }
        else
        {
            return true;
            //score is 21
        }
    }

    private static string InputName()
    {
        while (true)
        {
            string? userName = String.Empty;
            Console.Clear();
            Console.WriteLine("");
            AnsiConsole.Markup("[darkgoldenrod]Ange ditt namn: [/]");

            userName = Console.ReadLine();
            bool isAlpha = userName.All(Char.IsLetter);

            if (!isAlpha)
            {
                Console.Clear();
                Console.WriteLine("");
                AnsiConsole.Markup("[darkgoldenrod]Ditt namn får endast innehålla vanliga bokstäver.[/]");
                Console.WriteLine("");
                AnsiConsole.MarkupLine("[darkgoldenrod]Tryck vilken tangent som helst för att bekräfta.[/]");
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            if (string.IsNullOrEmpty(userName))
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("Du kan inte ha ett tomt namn.");
                Console.WriteLine("");
                Console.WriteLine("Tryck vilken tangent som helst för att bekräfta.");
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            if (isAlpha)
            {
                AnsiConsole.MarkupLine("Är du säker? [royalblue1](ja/nej)[/]");
            }
            if (UserInputIsYes())
            {
                return userName;
            }
        }
    }

    private static void Menu(string userInput, string nameOfUser, ref PlayerStatsDatabase playerStats)
    { 
        switch (userInput)
        {
            case "1":
                nameOfUser = InputName(); 
                playerCurrency currency = new playerCurrency(ref playerStats);
                int userMoney;
                do
                {
                    userMoney = currency.GetUserMoney(nameOfUser);
                    int userBet = currency.MakeBet(userMoney);
                    
                    if (Play21())
                    {
                        playerStats.RecordPlayerWin(nameOfUser, userMoney, userBet);
                    }
                    else
                    {
                        playerStats.RecordPlayerLoss(nameOfUser, userMoney, userBet);
                    }
                }
                    while (AskForRematch());
                return;

            case "2":
                Console.Clear();
                Console.WriteLine("");
                StreamReader file = new StreamReader("lastWinner.txt");
                string textLine = file.ReadLine();
                Console.WriteLine(textLine != "Ingen har spelat än" ? $"Senaste vinnaren var {textLine}." : textLine);
                file.Close();
                ReturnToMenu(); 
                break;
            case "3":
                Play21Rules();
                break;
            case "4":
                string userName = SearchStatistics().FirstCharToUpper();
                var statisticsResult = playerStats.GetOrCreateStats(userName);
                if (statisticsResult.Matches == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Personen finns inte.");
                    ReturnToMenu();
                    break;
                }
                Console.WriteLine($"Spelare: {userName} {statisticsResult}");
                ReturnToMenu();
                break;
            case "5": 
                Console.Clear();
                foreach (var user in playerStats.GetAllStats())
                {
                    Console.WriteLine($"{user.Name.FirstCharToUpper()} {user.Stats}\n");
                }
                var json = File.ReadAllText("playerStats.json");
                if (json != "{}")
                {
                    ResetStats(ref playerStats);
                }
                else
                {
                    Console.WriteLine("Det finns inget att visa här.");
                    ReturnToMenu();
                }
                break;
            case "6":
                StreamWriter sw = new StreamWriter("lastWinner.txt");
                sw.WriteLine("Ingen har spelat än");
                sw.Close();
                Environment.Exit(0);
                break;
        }
    }

    private static void ResetStats(ref PlayerStatsDatabase playerStats)
    {
        AnsiConsole.MarkupLine("[red1]Tryck Delete om du vill återställa statistiken.[/]");
        Console.WriteLine("");
        AnsiConsole.MarkupLine("[darkgoldenrod]   Tryck på vilken tangent som helst för att gå tillbaka.[/]");
        var input = Console.ReadKey();

        if (input.Key == ConsoleKey.Delete)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("");
            AnsiConsole.MarkupLine("                 [darkgoldenrod]Är du säker?[/][royalblue1](ja/nej)[/]");
            while (UserInputIsYes())
            {
                File.WriteAllText("playerStats.json", "{}");
                playerStats.ResetStats();
                break;
            }
        }   //resets stats and ask to return to menu
        else return;
    }

    private static bool AskForRematch()
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine("");
        while (true)
        {
            AnsiConsole.MarkupLine("             [lightgoldenrod2_1]Vill du spela igen? [/][royalblue1](ja/nej)[/]");
            return UserInputIsYes();
        }
    }

    private static void Play21Rules()
    {
        Console.Clear();
        Console.WriteLine("");
        AnsiConsole.MarkupLine("~     Spelet går ut på att man tar kort tills du har [orange1]21[/], eller så nära som möjligt, men inte mer.");
        AnsiConsole.MarkupLine("~     [orange1]Ess[/] är värt [orange1]1[/] poäng, [orange1]knekt,[/], [orange1]dam[/] och [orange1]kung[/] är alla värda [orange1]10[/] poäng.");
        AnsiConsole.MarkupLine("~     Det du bettar [skyblue2]dubblas om du vinner[/], om du förlorar så [skyblue2]förlorar du det som du bettat[/].");
        AnsiConsole.MarkupLine("~     Du får [skyblue2]10 poäng[/] för varje match du förlorar, så du inte fastnar på 0 poäng.");
        ReturnToMenu();
    }

    public static void ReturnToMenu()
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
        string nameOfUser = String.Empty;
        while (true)
        {   
            Console.Clear();
            AnsiConsole.MarkupLine("[dodgerblue1]                  Välkommen till[/] [deepskyblue2]21an![/]");
            AnsiConsole.MarkupLine("[orange1]  Välj ett alternativ nedan med hjälp av tangenterna 1-5.[/]");
            AnsiConsole.MarkupLine("[orange1]               Bekräfta med enter.[/]");
            Console.WriteLine("");
            AnsiConsole.MarkupLine("[darkgoldenrod]  1.[/] Spela 21an");
            AnsiConsole.MarkupLine("[darkgoldenrod]  2.[/] Senaste Vinnaren");
            AnsiConsole.MarkupLine("[darkgoldenrod]  3.[/] Spelets Regler");
            AnsiConsole.MarkupLine("[darkgoldenrod]  4.[/] Sök Statistiken");
            AnsiConsole.MarkupLine("[darkgoldenrod]  5.[/] Visa All Statistik");
            AnsiConsole.MarkupLine("[darkgoldenrod]  6.[/] Avsluta Spelet");
            string? userInput = Console.ReadLine();

            Menu(userInput, nameOfUser, ref playerStats);
        }
    }
}

