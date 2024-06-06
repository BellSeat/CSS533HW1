using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button registerButton;

    public GameObject popupLoginFailed;
    public GameObject popupRegisSucces;
    public GameObject popupRegisFailed;
    public Canvas canvas;

    private const string UserProfilesKey = "UserProfiles";
    private static readonly Regex inputRegex = new Regex("^[a-zA-Z0-9!@#$%^&*()_+=-]{8,}$");

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);
        registerButton.onClick.AddListener(OnRegisterButtonClick);
    }

    void OnLoginButtonClick()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (ValidateInput(username, password))
        {
            Login(username, HashPassword(password));
        }
        else
        {
            string errorMessage = "Login Failed!\n\nPlease enter a username and password longer than 8 characters.\nYou can use letters, numbers, and the following symbols: !@#$%^&*()_+=-";
            SpawnPopup(popupLoginFailed, errorMessage);
        }
    }

    void OnRegisterButtonClick()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (ValidateInput(username, password))
        {
            Register(username, HashPassword(password));
        }
        else
        {
            string errorMessage = "Registration Failed!\n\nPlease enter a username and password longer than 8 characters.\nYou can use letters, numbers, and the following symbols: !@#$%^&*()_+=-";
            SpawnPopup(popupRegisFailed, errorMessage);
        }
    }

    void Login(string username, string hashedPassword)
    {
        Dictionary<string, UserProfile> userProfiles = LoadAllUsers();
        if (userProfiles == null)
        {
            Debug.LogError("Failed to load user profiles. UserProfiles dictionary is null.");
            return;
        }

        if (userProfiles.ContainsKey(username))
        {
            UserProfile profile = userProfiles[username];
            if (profile.Password == hashedPassword)
            {
                Debug.Log("Login successful.");
                MainPlayerProfile.playerName = username;
                SceneManager.LoadScene("Main");
            }
            else
            {
                SpawnPopup(popupLoginFailed, "Login Failed!\n\nIncorrect username or password.");
            }
        }
        else
        {
            SpawnPopup(popupLoginFailed, "Login Failed!\n\nUsername does not exist.");
        }
    }

    void Register(string username, string hashedPassword)
    {
        Dictionary<string, UserProfile> userProfiles = LoadAllUsers();
        if (userProfiles == null)
        {
            Debug.LogError("Failed to load user profiles. UserProfiles dictionary is null.");
            userProfiles = new Dictionary<string, UserProfile>();
        }

        if (userProfiles.ContainsKey(username))
        {
            string errorMessage = "Registration Failed!\n\nUsername already taken.";
            SpawnPopup(popupRegisFailed, errorMessage);
        }
        else
        {
            UserProfile profile = new UserProfile(username, hashedPassword);
            userProfiles[username] = profile;
            SaveAllUsers(userProfiles);
            SpawnPopup(popupRegisSucces, "Registration Successful!");
        }
    }

    void SpawnPopup(GameObject popupPrefab, string message = null)
    {
        GameObject popupInstance = Instantiate(popupPrefab);
        popupInstance.transform.SetParent(canvas.transform, false);

        if (message != null)
        {
            TMP_Text messageText = popupInstance.transform.Find("Message").GetComponent<TMP_Text>();
            if (messageText != null)
            {
                messageText.text = message;
            }
        }

        RectTransform popupRectTransform = popupInstance.GetComponent<RectTransform>();
        popupRectTransform.anchoredPosition = Vector2.zero;
    }

    bool ValidateInput(string username, string password)
    {
        return inputRegex.IsMatch(username) && inputRegex.IsMatch(password);
    }

    string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    Dictionary<string, UserProfile> LoadAllUsers()
    {
        string json = PlayerPrefs.GetString(UserProfilesKey, "{}");
        if (string.IsNullOrEmpty(json))
        {
            return new Dictionary<string, UserProfile>();
        }

        Dictionary<string, UserProfile> userProfiles;
        try
        {
            userProfiles = JsonConvert.DeserializeObject<Dictionary<string, UserProfile>>(json);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error deserializing user profiles: " + ex.Message);
            userProfiles = new Dictionary<string, UserProfile>();
        }

        return userProfiles;
    }

    void SaveAllUsers(Dictionary<string, UserProfile> userProfiles)
    {
        string json = JsonConvert.SerializeObject(userProfiles);
        PlayerPrefs.SetString(UserProfilesKey, json);
        PlayerPrefs.Save();
    }
}
