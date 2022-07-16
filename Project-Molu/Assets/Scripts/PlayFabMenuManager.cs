using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;

using TMPro;

using UnityEngine.SceneManagement;

public class PlayFabMenuManager : MonoBehaviour
{
    //public static PlayFabMenuManager instance;


    public TMP_Text chrysoValueText;
    public TMP_Text emeraldValueText;
    public TMP_Text displayName;

    public TMP_Text lvText;
    public TMP_Text expText;


    private void Awake()
    {
        // Use for GetVirtualCurrencies
        //instance = this;
    }

    // Get Virtual Currencies
    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
        //PlayFabClientAPI.Get
    }
    void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        int chrysos = result.VirtualCurrency["CP"];
        chrysoValueText.text = chrysos.ToString();


        int emeralds = result.VirtualCurrency["EM"];
        emeraldValueText.text = emeralds.ToString();

    }

    // Get Play Profile
    void GetPlayerProfile(string playFabId)
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            PlayFabId = playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true
            }
        },
        OnGetPlayerProfileSuccess,
        OnError);
    }
    void OnGetPlayerProfileSuccess(GetPlayerProfileResult result)
    {

        Debug.Log("The player's DisplayName profile data is: " + result.PlayerProfile.DisplayName);
        displayName.text = result.PlayerProfile.DisplayName;
    }

    // Get Player Lv, Exp from User data(Player data)
    void GetUserData(string myPlayFabId)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        }, result => {
            Debug.Log("Got user data:");
            if (result.Data == null || !result.Data.ContainsKey("Lv")) Debug.Log("No Level");
            else
            {
                Debug.Log("Level: " + result.Data["Lv"].Value);
                lvText.text = result.Data["Lv"].Value;
            }
            if (result.Data == null || !result.Data.ContainsKey("Exp")) Debug.Log("No Exp");
            else
            {
                Debug.Log("Exp: " + result.Data["Exp"].Value);
                expText.text = result.Data["Exp"].Value+"/10";
            }
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }


    void OnError(PlayFabError error)
    {
        //Debug.Log("Error while logging in/creating account!");
        //Debug.Log(error.GenerateErrorReport());
        //messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }


    void Start()
    {
        GetVirtualCurrencies();
        GetPlayerProfile(PlayFabAuthManager.instance.playFabID);
        GetUserData(PlayFabAuthManager.instance.playFabID);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
