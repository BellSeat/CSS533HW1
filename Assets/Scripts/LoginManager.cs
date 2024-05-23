using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button registerButton;

    public GameObject popupLoginFailed;
    public GameObject popupRegisSucces;
    public Canvas canvas; 

    void Start()
    {
        Button loginBtn = loginButton.GetComponent<Button>();
        Button regisBtn = registerButton.GetComponent<Button>();
        loginBtn.onClick.AddListener(OnLoginButtonClick);
        regisBtn.onClick.AddListener(OnRegisterButtonClick);
    }

    void OnLoginButtonClick()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        StartCoroutine(Login(username, password));
    }

    void OnRegisterButtonClick()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        StartCoroutine(Register(username, password));
    }

    IEnumerator Login(string username, string password)
    {
        string url = "http://3.88.180.219/login/";
        string jsonBody = "{\"username\": \"" + username + "\", \"password\": \"" + password + "\"}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Login Error: " + request.error);
            SpawnPopup(popupLoginFailed);
        }
        else
        {
            Debug.Log("Login Response: " + request.downloadHandler.text);
            SceneManager.LoadScene("Main");
        }
    }

    IEnumerator Register(string username, string password)
    {
        string url = "http://3.88.180.219/register/";
        string jsonBody = "{\"username\": \"" + username + "\", \"password\": \"" + password + "\"}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Register Error: " + request.error);
        }
        else
        {
            Debug.Log("Register Response: " + request.downloadHandler.text);
            SpawnPopup(popupRegisSucces);
        }
    }



    void SpawnPopup(GameObject popupPrefab)
    {
        // Instantiate the popup prefab
        GameObject popupInstance = Instantiate(popupPrefab);

        // Set the popup as a child of the canvas
        popupInstance.transform.SetParent(canvas.transform, false);

        // Center the popup in the canvas
        RectTransform popupRectTransform = popupInstance.GetComponent<RectTransform>();
        popupRectTransform.anchoredPosition = Vector2.zero;
    }
}
