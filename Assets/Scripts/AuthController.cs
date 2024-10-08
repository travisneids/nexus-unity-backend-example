using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class AuthController : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField usernameInputField;
    public InputField passwordInputField;
    public Button registerButton;
    public Button loginButton;
    public Text feedbackText;

    private const string backendUrl = "";
    private string token = "";

    private void Start()
    {
        // Add listeners for buttons
        registerButton.onClick.AddListener(RegisterUser);
        loginButton.onClick.AddListener(LoginUser);
    }

    // Register user function
    public void RegisterUser()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Username and password required!";
            return;
        }

        StartCoroutine(RegisterCoroutine(username, password));
    }

    public void LoginUser()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Username and password required!";
            return;
        }

        StartCoroutine(LoginCoroutine(username, password));
    }

    private IEnumerator RegisterCoroutine(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(backendUrl + "/register", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            feedbackText.text = "Registration successful!";
        }
        else
        {
            feedbackText.text = "Error: " + www.error;
        }
    }

    private IEnumerator LoginCoroutine(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(backendUrl + "/login", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Extract token from response and save it
            TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(www.downloadHandler.text);
            SaveTokenSecurely(tokenResponse.token);
            feedbackText.text = "Login successful!";
        }
        else
        {
            feedbackText.text = "Error: " + www.error;
        }
    }

    // Save token securely using PlayerPrefs (encrypted) or use a more secure method on mobile platforms
    private void SaveTokenSecurely(string token)
    {
        this.token = token;

        // Option 1: Encrypt and save in PlayerPrefs (simplified)
        string encryptedToken = Encrypt(token);  // You should implement your own encryption here
        PlayerPrefs.SetString("auth_token", encryptedToken);

        // Option 2: Use secure storage (e.g., Keychain on iOS, Keystore on Android)
        // Use Unity's SecurePlayerPrefs or a third-party package for secure storage
    }

    private string RetrieveToken()
    {
        string encryptedToken = PlayerPrefs.GetString("auth_token", "");
        if (string.IsNullOrEmpty(encryptedToken))
        {
            return null;
        }

        return Decrypt(encryptedToken);
    }

    // Mock encryption function (replace with actual encryption logic)
    private string Encrypt(string data)
    {
        // Replace with actual encryption logic
        return data;  // Simplified for demo purposes
    }

    // Mock decryption function (replace with actual decryption logic)
    private string Decrypt(string data)
    {
        // Replace with actual decryption logic
        return data;  // Simplified for demo purposes
    }

    [System.Serializable]
    private class TokenResponse
    {
        public string token;
    }
}
