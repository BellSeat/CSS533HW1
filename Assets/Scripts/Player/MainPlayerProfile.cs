using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class MainPlayerProfile : MonoBehaviour
{
    [SerializeField] public static string playerName;
    [SerializeField] private static int playerLevel;
    [SerializeField] private int playerScore;
    [SerializeField] private static int playerEXP;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(downloadUserProfileFromServer());
    }

    private void OnGUI()
    {
        if (isPaused)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Game is paused");
            uploadUserProfileToServer();
        }
    }

    public void OnApplicationPause(bool pause)
    {
        isPaused = pause;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
        if (hasFocus)
        {
            downloadUserProfileFromServer();
        }
    }

    private void uploadUserProfileToServer()
    {
        UpdateExperienceOnServer(playerEXP);
        UpdateLevelsOnServer(playerLevel);
    }

    private IEnumerator downloadUserProfileFromServer()
    {
        string url = "http://3.88.180.219/get_user_details/";
        string jsonBody = "{\"username\": \"" + playerName + "\"}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Get User Profile Error: " + request.error);
        }
        else
        {
            Debug.Log("User Profile: " + request.downloadHandler.text);
            // Parse the response JSON to set the user data
            var userProfile = JsonUtility.FromJson<UserProfile>(request.downloadHandler.text);
            playerName = userProfile.username;
            playerEXP = userProfile.experience;
            playerLevel = userProfile.levels;
        }
    }

    private IEnumerator UpdateExperienceOnServer(int newExperience)
    {
        string url = "http://3.88.180.219/update_experience/";
        string jsonBody = "{\"username\": \"" + playerName + "\", \"experience\": " + newExperience + "}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Update Experience Error: " + request.error);
        }
        else
        {
            Debug.Log("Update Experience Response: " + request.downloadHandler.text);
        }
    }

    private IEnumerator UpdateLevelsOnServer(int newLevels)
    {
        string url = "http://3.88.180.219/update_levels/";
        string jsonBody = "{\"username\": \"" + playerName + "\", \"levels\": " + newLevels + "}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Update Levels Error: " + request.error);
        }
        else
        {
            Debug.Log("Update Levels Response: " + request.downloadHandler.text);
        }
    }

    public void setPlayerName(string name)
    {
        playerName = name;
    }

    public void setPlayerLevel(int level)
    {
        playerLevel = level;
    }

    public void setPlayerScore(int score)
    {
        playerScore = score;
    }

    public void setPlayerEXP(int exp)
    {
        playerEXP = exp;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public int getPlayerLevel()
    {
        return playerLevel;
    }

    public int getPlayerScore()
    {
        return playerScore;
    }

    public int getPlayerEXP()
    {
        return playerEXP;
    }

    public void addPlayerEXP(int exp)
    {
        playerEXP += exp;
        calculateLevel();
        uploadUserProfileToServer();
    }

    public void addPlayerScore(int score)
    {
        playerScore += score;
        uploadUserProfileToServer();
    }

    private void calculateLevel()
    {
        if (playerEXP >= 100)
        {
            int add = playerEXP / 100;
            playerLevel += add;
            playerEXP = playerEXP % 100;
        }
    }

    [System.Serializable]
    private class UserProfile
    {
        public string username;
        public int experience;
        public int levels;
    }
}
