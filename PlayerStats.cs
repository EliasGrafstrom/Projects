internal class PlayerStats
{
    public int Wins { get; set; }
    public int Matches { get; set; }

    public override string ToString()
    {
        return ($"\nAntal Matcher: {Matches} \nAntal Vunna Matcher: {Wins}n\") + {WinPercentage()};
    }

    public string WinPercentage()
    {
        int wins = Wins;
        int matches = Matches;

        double winPercentage = (wins / matches) * 100;
        return ($"Procentuellt antal vunna matcher: {winPercentage}");
    }
}