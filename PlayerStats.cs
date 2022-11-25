internal class PlayerStats
{
    public int Wins { get; set; }
    public int Matches { get; set; }

    public override string ToString()
    {
        return ($"\nAntal Matcher: {Matches} \nAntal Vunna Matcher: {Wins}\n{WinPercentage()}");
    }

    public string WinPercentage()
    {
        // Här kan du skippa 
        double wins = Wins;
        double matches = Matches;

        double winPercentage = (wins / matches) * 100;
        int winPercentageInt = Convert.ToInt32(winPercentage);
        return ($"Procentuellt antal vunna matcher: {winPercentageInt}%");
    }
}