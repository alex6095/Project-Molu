using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using PlayFab;
using PlayFab.ClientModels;

public class PlayFabContentsManager : MonoBehaviour
{

    public TMP_Text chrysoValueText;
    public TMP_Text emeraldValueText;

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

    void OnError(PlayFabError error)
    {
        //Debug.Log("Error while logging in/creating account!");
        //Debug.Log(error.GenerateErrorReport());
        //messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    // Start is called before the first frame update
    void Start()
    {
        GetVirtualCurrencies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
