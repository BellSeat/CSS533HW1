using UnityEngine;

public class MainPlayerProfile : MonoBehaviour
{
    public static string playerName;
    public PlayerDataManager playerDataManager;

    void Start()
    {
        playerDataManager.currentPlayerData = playerDataManager.GetUserProfile(playerName);
    }

    private void OnDestroy()
    {
        SaveUserProfile();
        playerDataManager.SaveAllPlayersData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveUserProfile();
            playerDataManager.SaveAllPlayersData();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveUserProfile();
            playerDataManager.SaveAllPlayersData();
        }
        else
        {
            playerDataManager.LoadAllPlayersData();
            LoadUserProfile();
        }
    }

    private void LoadUserProfile()
    {
        UserProfile profile = playerDataManager.GetUserProfile(playerName);
        if (profile != null)
        {
            playerDataManager.currentPlayerData = profile;
        }
        else
        {
            Debug.LogError("User profile not found for: " + playerName);
        }
    }

    private void SaveUserProfile()
    {
        if (playerDataManager.currentPlayerData != null)
        {
            playerDataManager.AddOrUpdateUserProfile(playerDataManager.currentPlayerData);
        }
    }

    public void setPlayerName(string name)
    {
        playerName = name;
    }

    public void setPlayerLevel(int level)
    {
        if (playerDataManager.currentPlayerData != null)
        {
            playerDataManager.currentPlayerData.Level = level;
            SaveUserProfile();
        }
    }

    public void setPlayerScore(int score)
    {
        if (playerDataManager.currentPlayerData != null)
        {
            playerDataManager.currentPlayerData.Score = score;
            SaveUserProfile();
        }
    }

    public void setPlayerEXP(int exp)
    {
        if (playerDataManager.currentPlayerData != null)
        {
            playerDataManager.currentPlayerData.Experience = exp;
            SaveUserProfile();
        }
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public int getPlayerLevel()
    {
        return playerDataManager.currentPlayerData != null ? playerDataManager.currentPlayerData.Level : 0;
    }

    public int getPlayerScore()
    {
        return playerDataManager.currentPlayerData != null ? playerDataManager.currentPlayerData.Score : 0;
    }

    public int getPlayerEXP()
    {
        return playerDataManager.currentPlayerData != null ? playerDataManager.currentPlayerData.Experience : 0;
    }

    public void addPlayerEXP(int exp)
    {
        if (playerDataManager.currentPlayerData != null)
        {
            playerDataManager.currentPlayerData.Experience += exp;
            calculateLevel();
            SaveUserProfile();
        }
    }

    public void addPlayerScore(int score)
    {
        if (playerDataManager.currentPlayerData != null)
        {
            playerDataManager.currentPlayerData.Score += score;
            SaveUserProfile();
        }
    }

    private void calculateLevel()
    {
        if (playerDataManager.currentPlayerData != null)
        {
            if (playerDataManager.currentPlayerData.Experience >= 100)
            {
                int add = playerDataManager.currentPlayerData.Experience / 100;
                playerDataManager.currentPlayerData.Level += add;
                playerDataManager.currentPlayerData.Experience %= 100;
            }
        }
    }
}
