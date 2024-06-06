using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResulltMenu : MonoBehaviour
{

    [SerializeField] private Button closeButton;
    [SerializeField] private PlayerDataManager playerDataManager;

    public bool isPractice = false;

    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(CloseButtonClicked);
        if (!isPractice)
        {
            playerDataManager = GameObject.Find("MainPlayer").GetComponent<PlayerDataManager>();
            playerDataManager.AddPlayerEXP(500);
            playerDataManager.AddPlayerScore(500);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseButtonClicked()
    {
        Debug.Log("Close Button Clicked");
        Destroy(this.gameObject);
    }
}
