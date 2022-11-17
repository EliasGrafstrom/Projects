using _21an;
using System.IO;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

internal class PlayerStatsDatabase
{
    private Dictionary<string, PlayerStats> _players;

    public PlayerStatsDatabase()
    {
        _players = InitializePlayerStats();
    }
    internal void RecordPlayerLoss(string nameOfUser)
    {
        var playerStats = GetPlayerStats(nameOfUser);
        playerStats.Matches++;
        SaveChanges();
    }

    internal void RecordPlayerWin(string nameOfUser)
    {
        var playerStats = GetPlayerStats(nameOfUser);
        playerStats.Matches++;
        playerStats.Wins++;
        SaveChanges();
        
    }
    public PlayerStats GetPlayerStats(string nameOfUser)
    {
        if (_players.TryGetValue(nameOfUser.ToLower(), out PlayerStats? stats))
        {
            return stats;
        }       
            PlayerStats newPerson = new PlayerStats();
            
            _players.Add (nameOfUser.ToLower(), newPerson);

        return newPerson;
    }   

    const string path = "playerStats.json";
    private void SaveChanges()
    {
        var jsonToWrite = JsonSerializer.Serialize(_players);
        File.WriteAllText(path, jsonToWrite);
    }
    
    private Dictionary<string, PlayerStats> InitializePlayerStats()
    {
        if (File.Exists(path))
        {
            var jsonToDictionary = JsonSerializer.Deserialize<Dictionary<string, PlayerStats>>(File.ReadAllText(path));
            return jsonToDictionary; 
        }       
        return new Dictionary<string, PlayerStats>();
    }

    public IEnumerable<(string Name, PlayerStats Stats)> GetAllStats()
    {
        return _players.Select(x => (x.Key, x.Value));
    } 
}