using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickScannerController : MonoBehaviour
{
    [SerializeField] private Button scanButton;
    // Start is called before the first frame update
    void Start()
    {
        scanButton = gameObject.GetComponent<Button>();
        Assert.IsNotNull(scanButton, "the button is not found");
        scanButton.onClick.AddListener(Scan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scan() { 
        Debug.Log("Scan Button Clicked");
        GameObject scanner = new GameObject();
        scanner.AddComponent<QuickScanner>();
        
    }
}
