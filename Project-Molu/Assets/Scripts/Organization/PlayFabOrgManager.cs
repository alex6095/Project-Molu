using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class PlayFabOrgManager : MonoBehaviour
{

    public TMP_Text chrysoValueText;
    public TMP_Text emeraldValueText;

    public CharacterBox[] characterBoxes;

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

    public void GetCharacters()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCharacterDataRecieved, OnError);

    }
    void OnCharacterDataRecieved(GetUserDataResult result)
    {
        Debug.Log("recieved characters data!");
        if (result.Data != null && result.Data.ContainsKey("Team"))
        {
            List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(result.Data["Team"].Value);
            for (int i=0; i < characterBoxes.Length; i++)
            {
                characterBoxes[i].SetUi(characters[i]);
                //Debug.Log(characters[i].stats.attackRange);
                // 잘 출력 됨
            }
        }
    }

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
        GetCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
