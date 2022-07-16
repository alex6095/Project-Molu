using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;

using TMPro;

using UnityEngine.SceneManagement;

public class PlayFabAuthManager : MonoBehaviour
{
    public static PlayFabAuthManager instance;

    public TMP_Text messageText;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    // Login Group
    public GameObject loginGroup;

    public GameObject nameWindow;
    public TMP_InputField nameInput;

    public string playFabID;

    public void Awake()
    {
        instance = this;
    }

    // Email Register/Login

    // Register
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

    // Login
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = inputEmail.text,
            Password = inputPassword.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    
    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in!";
        Debug.Log("Successful login/account create");

        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        else
            playFabID = result.PlayFabId; // result.InfoResultPayload.PlayerProfile.PlayerId;

        if (name == null)
        {
            loginGroup.SetActive(false);
            nameWindow.SetActive(true);

        }

        // 닉네임 설정 안되어 있음 -> 하고 Scene 이동
        // 되어있음 -> 그냥 Scene 이동

        // Scene 이동 -> MainMenu
        else
            SceneManager.LoadScene("MainMenu");

    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
        SceneManager.LoadScene("MainMenu");
    }

    // Reset Password

    // 후에 구현




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
