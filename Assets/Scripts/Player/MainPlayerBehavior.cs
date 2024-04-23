using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, MainCamera.transform.position.z);
    }
}
