using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class AuthController : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public Button registerButton;
    public Button loginButton;
    public TMP_Text feedbackText;

    private const string backendUrl = "http://localhost:5000/api/auth";
    private const string RegisterEndpoint = "/register";
    private const string LoginEndpoint = "/login";
    private string token = "";

    private void Start()
    {
        // Add listeners for buttons
        registerButton.onClick.AddListener(() => ProcessUserCredentials(RegisterEndpoint));
        loginButton.onClick.AddListener(() => ProcessUserCredentials(LoginEndpoint));
    }

    // Process registration or login
    private void ProcessUserCredentials(string endpoint)
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            UpdateFeedbackText("Username and password required!");
            return;
        }

        StartCoroutine(SendJsonRequest(new UserCredentials(username, password), backendUrl + endpoint, endpoint == LoginEndpoint));
    }

    // Send JSON request to server
    private IEnumerator SendJsonRequest(UserCredentials userData, string url, bool isLoginRequest)
    {
        // Convert user credentials to JSON
        string jsonPayload = JsonUtility.ToJson(userData);
        LogRequest(url, jsonPayload);

        // Create UnityWebRequest with JSON data
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonPayload);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return www.SendWebRequest();

        // Handle the response
        if (www.result == UnityWebRequest.Result.Success)
        {
            UpdateFeedbackText(isLoginRequest ? "Login successful!" : "Registration successful!");
            LogResponse(www);

            // If it's a login request, save the token
            if (isLoginRequest)
            {
                TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(www.downloadHandler.text);
                SaveTokenSecurely(tokenResponse.token);
            }
        }
        else
        {
            UpdateFeedbackText("Error: " + www.error);
            Debug.LogError("Error response: " + www.downloadHandler.text);
        }
    }

    // Update the feedback text
    private void UpdateFeedbackText(string message)
    {
        feedbackText.text = message;
    }

    // Save token securely using PlayerPrefs (encrypted) or use a more secure method on mobile platforms
    private void SaveTokenSecurely(string token)
    {
        this.token = token;
        string encryptedToken = Encrypt(token);  // Implement your own encryption here
        PlayerPrefs.SetString("auth_token", encryptedToken);
    }

    private string RetrieveToken()
    {
        string encryptedToken = PlayerPrefs.GetString("auth_token", "");
        return string.IsNullOrEmpty(encryptedToken) ? null : Decrypt(encryptedToken);
    }

    // Mock encryption function (replace with actual encryption logic)
    private string Encrypt(string data) => data; // Simplified for demo purposes

    // Mock decryption function (replace with actual decryption logic)
    private string Decrypt(string data) => data; // Simplified for demo purposes

    // Logging helpers
    private void LogRequest(string url, string payload)
    {
        Debug.Log($"Sending request to {url}");
        Debug.Log($"Payload: {payload}");
    }

    private void LogResponse(UnityWebRequest www)
    {
        Debug.Log($"Response code: {www.responseCode}");
        Debug.Log($"Response: {www.downloadHandler.text}");
    }
}
