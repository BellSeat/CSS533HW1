using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaScannerUIController : MonoBehaviour
{

    [SerializeField] private int remainingScans;
    [SerializeField] private Button scanButton;
    [SerializeField] private Color NormalColor, HighlightedColor, SelectedColor,PressedColor, DisabledColor;
    [SerializeField] private bool standby;
    // Start is called before the first frame update
    void Start()
    {
        // for testing purposes, set remaining scans to 3
        remainingScans = 3;

        // get the button component
        scanButton = GetComponent<Button>();
        Assert.IsNotNull(scanButton, "Button component not found");

        // set the button colors
        NormalColor = Color.grey;
        HighlightedColor = Color.white;
        SelectedColor = Color.blue;
        PressedColor = Color.red;
        DisabledColor = Color.black;

        // set the button colors
        ColorBlock colorBlock = scanButton.colors;
        colorBlock.normalColor = NormalColor;
        colorBlock.highlightedColor = HighlightedColor;
        colorBlock.selectedColor = SelectedColor;
        colorBlock.pressedColor = PressedColor;
        colorBlock.disabledColor = DisabledColor;


        scanButton.onClick.AddListener(ScanButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void ScanButtonClicked()
    {
        Debug.Log("Scan Button Clicked");
        // if there are remaining scans
        if (remainingScans > 0)
        {
            standby = true;
            // create a new AreaScanner object
            GameObject areaScanner = new GameObject();
            
            SpriteRenderer render = areaScanner.AddComponent<SpriteRenderer>();

            areaScanner.AddComponent<AreaScanner>();
            //areaScanner.name = "AreaScanner";
            remainingScans--;
        }
        else
        {
            // disable the button if there are no remaining scans
            scanButton.interactable = false;
        }
    }

}
