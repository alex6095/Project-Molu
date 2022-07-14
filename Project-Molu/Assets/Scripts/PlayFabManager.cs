using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;

using TMPro;

public class PlayFabManager : MonoBehaviour
{
    public TMP_Text messageText;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    // Email Register/Login
    public void RegisterButton()
    {
        if (inputPassword.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = inputEmail.text,
            Password = inputPassword.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in!";
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = inputEmail.text,
            Password = inputPassword.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in!";
        Debug.Log("Successful login/account create");
    }


    // Start is called before the first frame update
    void Start()
    {
        //Login();
    }

    // Anonymous Login/Register
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);

    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
    }

    // OnError

    void OnError(PlayFabError error)
    {
        //Debug.Log("Error while logging in/creating account!");
        //Debug.Log(error.GenerateErrorReport());
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
