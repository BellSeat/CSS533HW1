using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// This Class is checking if any object is invisible when use click the mouse or 
// click screen, then from the click position with a radius of 3, the object will be visible
public class AreaScanner : MonoBehaviour, IPointerClickHandler
{


    // Start is called before the first frame update
    void Start()
    {
        Vector2 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(click, 3);
        gameObject.AddComponent<ScannerTemplate>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        
        Debug.Log("Mouse Position: " + Input.mousePosition);
        Vector2 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(click, 3);
        foreach (Collider2D collider in colliders)
        {
            collider.gameObject.SetActive(true);
        }
    
    }

}
