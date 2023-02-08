using _21an;
using System.IO;
using System.Text.Json;

internal class PlayerStatsDatabase
{
    private Dictionary<string, PlayerStats> _players;
    public PlayerStatsDatabase()
    {
        _players = LoadPlayerStatsFromFile();
    }
    internal void RecordPlayerLoss(string nameOfUser, int userMoney, int userBet)
    {
        StreamWriter sw = new StreamWriter("lastWinner.txt");
        var playerStats = GetOrCreateStats(nameOfUser);
        playerStats.Matches++;
        playerStats.Credits = userMoney -= userBet - 10;
        sw.WriteLine("Datorn");
        sw.Close();
        SaveChanges();
    } //records player loss

    internal void RecordPlayerWin(string nameOfUser, int userMoney, int userBet)
    {
        StreamWriter sw = new StreamWriter("lastWinner.txt");
        var playerStats = GetOrCreateStats(nameOfUser);
        playerStats.Matches++;
        playerStats.Wins++;
        playerStats.Credits = userMoney + userBet;
        sw.WriteLine(nameOfUser);
        sw.Close();
        SaveChanges();
    } //records player win
    public PlayerStats GetOrCreateStats(string nameOfUser)
    {
        if (_players.TryGetValue(nameOfUser.ToLower(), out PlayerStats? stats))
        {
            return stats;
        }
        PlayerStats newPerson = new PlayerStats();
            
        _players.Add(nameOfUser.ToLower(), newPerson);

        return newPerson;
    } //returns the stats of a person, if the person doesn't exist, it creates a new person in the _players dictionary.

    const string path = "playerStats.json";
    public void SaveChanges()
    {
        var jsonToWrite = JsonSerializer.Serialize(_players);
        File.WriteAllText(path, jsonToWrite);
    } //saves the stats and sends it to the json file.

    internal void ResetStats()
    {
        _players.Clear();
    }

    private Dictionary<string, PlayerStats> LoadPlayerStatsFromFile()
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