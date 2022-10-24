using System.Security.Cryptography.X509Certificates;

internal class PlayerStatsDatabase
{
    private Dictionary<string, PlayerStats> _players;

    public PlayerStatsDatabase()
    {
        _players = new Dictionary<string, PlayerStats>();
    }
    internal void RecordPlayerLoss(string nameOfUser)
    {
        var playerStats = GetPlayerStats(nameOfUser);
    }

    internal void RecordPlayerWin(string nameOfUser)
    {
        
    }
    private PlayerStats GetPlayerStats(string nameOfUser)
    {
        if (_players.TryGetValue(nameOfUser, out PlayerStats stats))
        {
                
        }
    }
}