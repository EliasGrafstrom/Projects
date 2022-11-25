using _21an;
using System.Runtime.InteropServices;
using Spectre.Console;
using static _21an.Cards;
using Terminal.Gui;

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

    public static bool Play21(string nameOfUser)
    {
        playerCurrency currency = new playerCurrency();
        
        Deck deck = new Deck();
        Console.Clear();

        int bet = currency.MakeBet();
        currency.GetUserMoney(nameOfUser, bet);

        var userFirstCard = deck.Draw();
        var userSecondCard = deck.Draw();
        var computerFirstCard = deck.Draw();
        var computerSecondCard = deck.Draw();

        int userFirstValue = (int)userFirstCard.Value;
        int userSecondValue = (int)userSecondCard.Value;
        int computerFirstValue = (int)computerFirstCard.Value;
        int computerSecondValue = (int)computerSecondCard.Value;

        int userScore = userFirstValue + userSecondValue;
        int computerScore = computerFirstValue + computerSecondValue;

        if (userFirstCard != userSecondCard)
        {
            AnsiConsole.MarkupLine($"Du fick korten [chartreuse3_1]{userFirstCard}[/] och [chartreuse3_1]{userSecondCard}[/]. Du har [chartreuse3_1]{userScore}[/] poäng.");
        }

        if (computerFirstCard != computerSecondCard)
        {
            AnsiConsole.MarkupLine($"Datorn fick korten [chartreuse3_1]{computerFirstCard}[/] och [chartreuse3_1]{computerSecondCard}[/]. Datorn har nu [chartreuse3_1]{computerScore}[/] poäng.");
        }

        AnsiConsole.MarkupLine($"Vill du ta ett till kort? [mediumspringgreen](ja/nej)[/]");
        while (UserInputIsYes())
        {
            Console.Clear();
            if (userScore < 21)
            {
                var newUserCard = deck.Draw();
                int newUserCardValue = (int)newUserCard.Value;
                userScore += newUserCardValue;

                AnsiConsole.MarkupLine($"Du fick kortet [chartreuse3_1]{newUserCard}[/]. Du har nu [chartreuse3_1]{userScore}[/] poäng.");
                if (userScore < 21)
                {
                    AnsiConsole.MarkupLine($"Datorn har fortfarande [chartreuse3_1]{computerScore}[/] poäng.");
                    AnsiConsole.MarkupLine("Vill du ta ett till kort? [mediumspringgreen](ja/nej)[/]");
                }

                else if (userScore > 21)
                {
                    AnsiConsole.MarkupLine($"Otur! Du fick för mycket poäng. Datorn har vunnit med [chartreuse3_1]{computerScore}[/] jämfört mot dina [chartreuse3_1]{userScore}[/].");
                    ReturnToMenu();
                    return false;
                }
                else if (userScore == 21)
                {
                    AnsiConsole.MarkupLine("Grattis. Du har vunnit spelet då du fick [chartreuse3_1]21[/] poäng.");
                    ReturnToMenu();
                    return true;
                }
            }
        }


        while (computerScore < 21 && computerScore < userScore)
        {
            int noDelay = 1;

            var newPCCard = deck.Draw();
            int newPCCardValue = (int)newPCCard.Value;

            if (computerScore > 21)
            {
                AnsiConsole.MarkupLine($"Du vann! Grattis. Datorn fick [chartreuse3_1]{computerScore}[/], vilket är mer än [chartreuse3_1]21[/].");
                ReturnToMenu();
                return true;
            }

            else if (computerScore == 21)
            {
                AnsiConsole.MarkupLine($"Datorn plockade upp kortet [chartreuse3_1]{newPCCardValue}[/]. Datorn har nu [chartreuse3_1]{computerScore}[/] poäng, och vann därför.");
                ReturnToMenu();
                return false;
            }

            else
            {
                computerScore += newPCCardValue;
                AnsiConsole.MarkupLine($"Datorn plockade upp kortet [chartreuse3_1]{newPCCard}[/]. Datorn har nu [chartreuse3_1]{computerScore}[/] poäng.");
            }

            if (noDelay == 1)
            {
                Thread.Sleep(1300);
                noDelay++;
            }
        }

        if (computerScore > 21)
        {
            AnsiConsole.MarkupLine($"Du har vunnit med [chartreuse3_1]{userScore}[/] jämfört mot datorn som fick [chartreuse3_1]{computerScore}[/]. Grattis!");
            ReturnToMenu();
            return true;
        }

        if (userScore <= 21 && userScore > computerScore)
        {
            AnsiConsole.MarkupLine($"Du har vunnit med [chartreuse3_1]{userScore}[/] jämfört mot datorns [chartreuse3_1]{computerScore}[/]. Grattis!");
            ReturnToMenu();
            return true;
        }
        else if (computerScore <= 21)
        {
            AnsiConsole.MarkupLine($"Datorn har vunnit med [chartreuse3_1]{computerScore}[/] jämfört mot dina [chartreuse3_1]{userScore}[/].");
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

    private static void Play21Rules()
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine("Spelet går ut på att man tar kort tills du har 21, eller så nära som möjligt, men inte mer.");
        Console.WriteLine("Ess är värt 1 poäng, knekt 11, dam 12 och kung 13.");
        ReturnToMenu();
    }

    private static void ReturnToMenu()
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
        string inputName = Console.ReadLine().ToLower();
        return inputName;
    }


    public static void Main()
    {
        playerCurrency credits = new playerCurrency();
        int userBet = 2;
        string name = "Hampus";
        var f = credits.GetUserMoney(name, userBet);
        Console.WriteLine(f);

        string lastWinner = String.Empty;
        string nameOfUser; 
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

            PlayerStatsDatabase playerStatsDatabase = new PlayerStatsDatabase();

            switch (userInput)
            {
                case "1":
                    nameOfUser = GetName();
                    if (Play21(nameOfUser))
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
                    AnsiConsole.MarkupLine($"Senaste vinnaren var [darkgoldenrod]{lastWinner}[/].");
                    ReturnToMenu();
                    break;
                case "3":
                    Play21Rules();
                    break;
                case "4":
                    string userName = SearchStatistics().FirstCharToUpper();
                    var statisticsResult = playerStatsDatabase.GetPlayerStats(userName);
                    Console.WriteLine($"Spelare: {userName} {statisticsResult}");
                    ReturnToMenu();
                    break;
                case "5":
                    Console.Clear();
                    foreach (var user in playerStatsDatabase.GetAllStats())
                    {
                        Console.WriteLine($"{user.Name.FirstCharToUpper()} {user.Stats}\n");
                    }
                    ReturnToMenu();
                    break;
                case "6":
                    return;
            }
        }
    }
}
