using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoutManager : MonoBehaviour
{
    public Button logoutButton;
    public PlayerDataManager playerDataManager;

    void Start()
    {
        // Add listener to the logout button
        logoutButton.onClick.AddListener(OnLogoutButtonClick);
    }

    void OnLogoutButtonClick()
    {
        // Save all player data
        SavePlayerData();

        // Clear the player name
        MainPlayerProfile.playerName = "";

        // Load the Login scene
        SceneManager.LoadScene("Login");
    }

    void SavePlayerData()
    {
        if (playerDataManager != null)
        {
            playerDataManager.SaveAllPlayersData();
        }
    }
}
