internal class PlayerStats
{
    public int Wins { get; set; }
    public int Matches { get; set; }
    public int Credits { get; set; }
    public override string ToString()
    {
        return ($"\nAntal Matcher: {Matches} \nAntal Vunna Matcher: {Wins}\n{WinPercentage()} \nAntal Credits: {Credits}");
    }
    
    public string WinPercentage()
    {
        double wins = Wins;
        double matches = Matches;
        double winPercentage = (wins / matches) * 100;
        int winPercentageInt = Convert.ToInt32(winPercentage);
        return ($"Procentuellt antal vunna matcher: {winPercentageInt}%");
    }
}