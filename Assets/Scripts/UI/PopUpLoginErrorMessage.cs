using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpLoginErrorMessage : MonoBehaviour
{
    public GameObject popupMessage; // The popup message GameObject
    public Button okButton; // The OK button

    void Start()
    {
        okButton.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        Destroy(popupMessage);
    }
}
