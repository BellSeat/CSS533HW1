[System.Serializable]
public class UserProfile
{
    public string Username;
    public string Password;
    public int Experience;
    public int Level;
    public int Score;
    public bool[] GamesOwned;
    public bool PracticeMode;

    public UserProfile(string username, string password)
    {
        Username = username;
        Password = password;
        Experience = 0;
        Level = 1;
        Score = 0;
        GamesOwned = new bool[3];
        GamesOwned[0] = true; // Make the first game available for everyone
        PracticeMode = false;
    }
}
