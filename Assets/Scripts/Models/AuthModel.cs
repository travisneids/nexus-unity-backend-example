using System;

[System.Serializable]
public class UserCredentials
{
    public string username;
    public string password;

    public UserCredentials(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}

[System.Serializable]
public class TokenResponse
{
    public string token;
}