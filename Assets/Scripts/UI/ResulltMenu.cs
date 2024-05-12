using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResulltMenu : MonoBehaviour
{

    [SerializeField] private Button closeButton;
    [SerializeField] private MainPlayerProfile mainPlayerProfile;
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(CloseButtonClicked);
        mainPlayerProfile = GameObject.Find("MainPlayer").GetComponent<MainPlayerProfile>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseButtonClicked()
    {
        Debug.Log("Close Button Clicked");
        Destroy(this.gameObject);
        mainPlayerProfile.addPlayerEXP(500);
        mainPlayerProfile.addPlayerScore(500);
    }
}
