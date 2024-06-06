using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerDataManager : MonoBehaviour
{
    private const string UserProfilesKey = "UserProfiles";
    public Dictionary<string, UserProfile> UserProfiles;
    public UserProfile currentPlayerData;

    void Start()
    {
        LoadAllPlayersData();
    }

    void Awake()
    {
        LoadAllPlayersData();
    }

    void OnDestroy()
    {
        SaveAllPlayersData();
    }

    void OnDisable()
    {
        SaveAllPlayersData();
    }

    public void LoadAllPlayersData()
    {
        string json = PlayerPrefs.GetString(UserProfilesKey, "{}");
        UserProfiles = JsonConvert.DeserializeObject<Dictionary<string, UserProfile>>(json);
    }

    public void SaveAllPlayersData()
    {
        string json = JsonConvert.SerializeObject(UserProfiles);
        PlayerPrefs.SetString(UserProfilesKey, json);
        PlayerPrefs.Save();
    }

    public UserProfile GetUserProfile(string username)
    {
        if (UserProfiles.ContainsKey(username))
        {
            Debug.Log("Profile not null");
            return UserProfiles[username];
        }
        Debug.Log("Profile null");
        return null;
    }

    public void AddOrUpdateUserProfile(UserProfile profile)
    {
        UserProfiles[profile.Username] = profile;
    }

    public bool CheckPracticeOn()
    {
        if (currentPlayerData != null)
        {
            return currentPlayerData.PracticeMode;
        }
        return false;
    }

    public bool CheckGameOn(int gameNumber)
    {
        if (currentPlayerData != null)
        {
            return currentPlayerData.GamesOwned[gameNumber];
        }
        return false;
    }

    public int getPlayerEXP()
    {
        if (currentPlayerData != null)
        {
            return currentPlayerData.Experience;
        }
        return 0;
    }

    public int getPlayerLevel()
    {
        if (currentPlayerData != null)
        {
            return currentPlayerData.Level;
        }
        return 0;
    }

    public int getPlayerScore()
    {
        if (currentPlayerData != null)
        {
            return currentPlayerData.Score;
        }
        return 0;
    }

    public void AddPlayerEXP(int amount)
    {
        if (currentPlayerData != null)
        {
            currentPlayerData.Experience += amount;
            CalculateLevel();
            AddOrUpdateUserProfile(currentPlayerData);
        }
    }

    public void AddPlayerScore(int amount)
    {
        if (currentPlayerData != null)
        {
            currentPlayerData.Score += amount;
            AddOrUpdateUserProfile(currentPlayerData);
        }
    }

    public void SpendPlayerScore(int amount)
    {
        if (currentPlayerData != null)
        {
            currentPlayerData.Score -= amount;
            AddOrUpdateUserProfile(currentPlayerData);
        }
    }

    public void TurnOnGame(int gameNumber)
    {
        if (currentPlayerData != null && gameNumber >= 0 && gameNumber < currentPlayerData.GamesOwned.Length)
        {
            currentPlayerData.GamesOwned[gameNumber] = true;
            AddOrUpdateUserProfile(currentPlayerData);
        }
    }

    public void TurnOnPracticeMode()
    {
        currentPlayerData.PracticeMode = true;
        AddOrUpdateUserProfile(currentPlayerData);
    }

    public void CalculateLevel()
    {
        if (currentPlayerData != null)
        {
            currentPlayerData.Level = currentPlayerData.Experience / 1000 + 1;
            AddOrUpdateUserProfile(currentPlayerData);
        }
    }
}
