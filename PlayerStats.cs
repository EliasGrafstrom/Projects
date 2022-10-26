internal class PlayerStats
{
    public int Wins { get; set; }
    public int Matches { get; set; }

    public override string ToString()
    {
        return ($"\nAntal Matcher: {Matches} \nAntal Vunna Matcher: {Wins}");
    }
}