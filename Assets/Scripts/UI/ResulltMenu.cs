using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResulltMenu : MonoBehaviour
{

    [SerializeField] private Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(CloseButtonClicked);
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
