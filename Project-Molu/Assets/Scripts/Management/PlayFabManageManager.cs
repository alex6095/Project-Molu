using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class PlayFabManageManager : MonoBehaviour
{
    public static PlayFabManageManager instance;

    public TMP_Text chrysoValueText;
    public TMP_Text emeraldValueText;

    public CharacterBox[] characterBoxes;

    public List<Character> Team;

    public void Awake()
    {
        instance = this;
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



    // Function for Setting Characters
    //public void SaveCharacters()
    //{
    //    List<Character> characters = new List<Character>();
    //    foreach (var item in characterBoxes)
    //    {
    //        characters.Add(item.ReturnClass());
    //    }
    //    var request = new UpdateUserDataRequest
    //    {
    //        Data = new Dictionary<string, string>
    //        {
    //            {"Team", JsonConvert.SerializeObject(characters) }
    //        }
    //    };
    //    PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError); 
    //}

    public void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Successful user data send!");
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
