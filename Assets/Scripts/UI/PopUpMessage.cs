using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PopUpMessage : MonoBehaviour, IPointerDownHandler
{
    // 

    [SerializeField] private TMP_Text message;
    [SerializeField] private int duration = 3;
    // Start is called before the first frame update

    // Set the message of the pop up
    public void setMessage(string message)
    {
        this.message.text = message;
        StartCoroutine(runAndDesitory());
    }

    public void setDuration(int duration)
    {
        this.duration = duration;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Destroying");
        Destroy(this.gameObject);
    
    }

    IEnumerator runAndDesitory() 
    {
       yield return new WaitForSeconds(duration);
       Destroy(this.gameObject);
    }



}
