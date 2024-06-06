using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProfilerUIController : MonoBehaviour
{
    [SerializeField] public PlayerDataManager playerDataManager;
    [SerializeField] private PlayerData currentPlayerData;
    [SerializeField] private TMP_Text playerNameText, playerEXP, playerLevel, playerScore;
    [SerializeField] private Button profileButton;
    [SerializeField] private bool isVisible  = false;
    [SerializeField] private GameObject profilePannel;
    private string playerNamePreText, playerEXPText, playerLevelText, playerScoreText;
    // Start is called before the first frame update
    void Start()
    {
        profileButton = GetComponent<Button>();
        profileButton.onClick.AddListener(toggleProfile);
        playerNamePreText = "Player Name: ";
        playerEXPText = "Player EXP: ";
        playerLevelText = "Player Level: ";
        playerScoreText = "Player Coins: ";
    }

    // display/hide player profile
    public void toggleProfile()
    {
        isVisible = !isVisible;
        if(isVisible)
        {
            playerNameText.text = playerNamePreText + MainPlayerProfile.playerName;
            playerEXP.text = playerEXPText + playerDataManager.getPlayerEXP();
            playerLevel.text = playerLevelText + playerDataManager.getPlayerLevel();
            playerScore.text = playerScoreText + playerDataManager.getPlayerScore();
        }
        else
        {
            playerNameText.text = "";
            playerEXP.text = "";
            playerLevel.text = "";
            playerScore.text = "";
        }

        profilePannel.SetActive(isVisible);
    }

}
