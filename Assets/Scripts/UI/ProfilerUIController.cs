using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProfilerUIController : MonoBehaviour
{
    [SerializeField] private MainPlayerProfile playerProfile;
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
        playerScoreText = "Player Score: ";
    }

    // display/hide player profile
    public void toggleProfile()
    {
        isVisible = !isVisible;
        if(isVisible)
        {
            playerNameText.text = playerNamePreText + playerProfile.getPlayerName();
            playerEXP.text = playerEXPText + playerProfile.getPlayerEXP().ToString();
            playerLevel.text = playerLevelText + playerProfile.getPlayerLevel().ToString();
            playerScore.text = playerScoreText + playerProfile.getPlayerScore().ToString();
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
