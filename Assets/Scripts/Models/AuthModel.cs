using System;

[System.Serializable]
public class UserCredentials
{
    public string playerName;
    public string password;

    public UserCredentials(string playerName, string password)
    {
        this.playerName = playerName;
        this.password = password;
    }
}

[System.Serializable]
public class TokenResponse
{
    public string token;
}