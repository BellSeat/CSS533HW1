using UnityEngine;
using UnityEngine.UI;

public class PanelManagers : MonoBehaviour
{
    public Button shopBtn, practiceBtn;
    public GameObject shopPanel, practicePanel;
    public Button shopGame1, shopGame2, shopGame3, shopPractice;
    public Button pracGame1, pracGame2, pracGame3;

    public PlayerDataManager playerDataManager;

    void Start()
    {
        // Add listeners to buttons
        shopBtn.onClick.AddListener(OnShopButtonClick);
        practiceBtn.onClick.AddListener(OnPracticeButtonClick);

        // Add listeners to shop game buttons
        shopGame1.onClick.AddListener(() => OnShopGameButtonClick(0)); // Note: Changed to 0-based indexing
        shopGame2.onClick.AddListener(() => OnShopGameButtonClick(1)); // Note: Changed to 0-based indexing
        shopGame3.onClick.AddListener(() => OnShopGameButtonClick(2)); // Note: Changed to 0-based indexing
        shopPractice.onClick.AddListener(() => OnShopPracticeButtonClicked());

        // Initialize panels to be inactive
        shopPanel.SetActive(false);
        practicePanel.SetActive(false);

        // Update button states based on game ownership
        UpdateShopGameButtons();
        UpdatePracticeGameButtons();
    }

    void OnShopButtonClick()
    {
        // Toggle the shop panel visibility
        shopPanel.SetActive(!shopPanel.activeSelf);
        // Ensure the practice panel is hidden
        practicePanel.SetActive(false);

        // Check availability of games and update button states
        if (shopPanel.activeSelf)
        {
            UpdateShopGameButtons();
        }
    }

    void OnShopPracticeButtonClicked()
    {
        // Turn on the game and subtract 500 score
        if (playerDataManager.getPlayerScore() >= 500)
        {
            playerDataManager.TurnOnPracticeMode();
            playerDataManager.SpendPlayerScore(500);

            UpdateShopGameButtons();
        }
        else
        {
            Debug.Log("Not enough gold to purchase practice mode.");
        }
    }

    void OnPracticeButtonClick()
    {
        // Toggle the practice panel visibility
        practicePanel.SetActive(!practicePanel.activeSelf);
        // Ensure the shop panel is hidden
        shopPanel.SetActive(false);

        // Check availability of games and update button states
        if (practicePanel.activeSelf)
        {
            UpdatePracticeGameButtons();
        }
    }

    void OnShopGameButtonClick(int gameNumber)
    {
        // Turn on the game and subtract 500 score
        if (playerDataManager.getPlayerScore() >= 500)
        {
            playerDataManager.TurnOnGame(gameNumber);
            playerDataManager.SpendPlayerScore(500);

            UpdateShopGameButtons();
        }
        else
        {
            Debug.Log("Not enough gold to purchase the game.");
        }
    }

    void UpdateShopGameButtons()
    {
        // Check game availability and set buttons active/inactive accordingly
        shopGame1.gameObject.SetActive(!playerDataManager.CheckGameOn(0)); // Note: Changed to 0-based indexing
        shopGame2.gameObject.SetActive(!playerDataManager.CheckGameOn(1)); // Note: Changed to 0-based indexing
        shopGame3.gameObject.SetActive(!playerDataManager.CheckGameOn(2)); // Note: Changed to 0-based indexing
        shopPractice.gameObject.SetActive(!playerDataManager.CheckPracticeOn()); 
    }

    void UpdatePracticeGameButtons()
    {
        // Check game availability and set buttons active/inactive accordingly
        pracGame1.gameObject.SetActive(playerDataManager.CheckGameOn(0)); // Note: Changed to 0-based indexing
        pracGame2.gameObject.SetActive(playerDataManager.CheckGameOn(1)); // Note: Changed to 0-based indexing
        pracGame3.gameObject.SetActive(playerDataManager.CheckGameOn(2)); // Note: Changed to 0-based indexing
    }
}
