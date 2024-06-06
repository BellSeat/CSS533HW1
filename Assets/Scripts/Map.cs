using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class Map : MonoBehaviour
{
    public string apiKey;
    public float lat = -33.85660618894087f;
    public float lon = 151.21500701957325f;
    public int zoom = 12;
    public enum resolution { low = 1, high = 2 };
    public resolution mapResolution = resolution.low;
    public enum type { roadmap, satellite, hybrid, terrain };
    public type mapType = type.roadmap;
    private string url = "";
    private int mapWidth;
    private int mapHeight;
    //private bool mapIsLoading = false;

    private string apiKeyLast;
    private float latLast = -33.85660618894087f;
    private float lonLast = 151.21500701957325f;
    private int zoomLast = 12;
    private resolution mapResolutionLast = resolution.low;
    private type mapTypeLast = type.roadmap;
    private bool updateMap = true;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        mapWidth = Screen.width;
        mapHeight = Screen.height;
        StartCoroutine(RequestLocationAndMap());
    }

    // Update is called once per frame
    void Update()
    {
        if (updateMap && (apiKeyLast != apiKey || !Mathf.Approximately(latLast, lat) || !Mathf.Approximately(lonLast, lon) || zoomLast != zoom || mapResolutionLast != mapResolution || mapTypeLast != mapType))
        {
            StartCoroutine(GetGoogleMap());
            updateMap = false;
        }
    }

    IEnumerator RequestLocationAndMap()
    {
        while (true)
        {
            if (Input.location.isEnabledByUser)
            {
                Input.location.Start();

                int maxWait = 20;
                while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
                {
                    yield return new WaitForSeconds(1);
                    maxWait--;
                }

                if (maxWait < 1)
                {
                    Debug.Log("Timed out");
                    yield break;
                }

                if (Input.location.status == LocationServiceStatus.Failed)
                {
                    Debug.Log("Unable to determine device location");
                    yield break;
                }
                else
                {
                    lat = Input.location.lastData.latitude;
                    lon = Input.location.lastData.longitude;
                    updateMap = true;
                }
                Input.location.Stop();
            }
            else
            {
                Debug.Log("Location services are not enabled");
            }

            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator GetGoogleMap()
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon + "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + mapResolution + "&maptype=" + mapType + "&key=" + apiKey;
        //mapIsLoading = true;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + www.error);
        }
        else
        {
            //mapIsLoading = false;
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            spriteRenderer.sprite = Sprite.Create(texture, rect, pivot);

            // Adjust the size of the sprite to fill the screen
            float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            spriteRenderer.size = new Vector2(worldScreenWidth, worldScreenHeight);

            apiKeyLast = apiKey;
            latLast = lat;
            lonLast = lon;
            zoomLast = zoom;
            mapResolutionLast = mapResolution;
            mapTypeLast = mapType;
            updateMap = true;
        }
    }
}
