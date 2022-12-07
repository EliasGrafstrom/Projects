using _21an;
using System.IO;
using System.Reflection.Metadata;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

internal class PlayerStatsDatabase
{
    private Dictionary<string, PlayerStats> _players;
    public PlayerStatsDatabase()
    {
        _players = LoadPlayerStatsFromFile();
    }
    internal void RecordPlayerLoss(string nameOfUser, int userMoney, int userBet)
    {
        var playerStats = GetPlayerStats(nameOfUser);
        playerStats.Matches++;
        playerStats.Credits = userMoney -= userBet - 10;
        SaveChanges();
    } //records player loss

    internal void RecordPlayerWin(string nameOfUser, int userMoney, int userBet)
    {
        var playerStats = GetPlayerStats(nameOfUser);
        playerStats.Matches++;
        playerStats.Wins++;
        playerStats.Credits = userMoney + userBet;
        SaveChanges();
    } //records player win
    public PlayerStats GetPlayerStats(string nameOfUser)
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
    private void SaveChanges()
    {
        var jsonToWrite = JsonSerializer.Serialize(_players);
        File.WriteAllText(path, jsonToWrite);
    } //saves the stats and sends it to the json file.

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