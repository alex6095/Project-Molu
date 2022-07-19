using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
public class GridViewControl : MonoBehaviour
{

    public GameObject Content;    
    //private GameObject UnitParent;

    //public CharacterBox[] characterBoxes;

    void Start()
    {
        

        GetCharacters();

        

        // for(int i=1; i<=20; i++)
        // {
        //     GameObject UnitParent = Instantiate(UnitParent) as GameObject;
        //     UnitParent.SetActive(true);

        //     UnitParent.GetComponent<GridViewItem>().SetUI("히비키","Lv.56",Resources.Load<Sprite>("Images/Sample_Skill_Image"));

        //     UnitParent.transform.SetParent(UnitParent.transform.parent,false);
        // }
    }
    public void GetCharacters()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCharacterDataRecieved, OnError);
    }
    void OnCharacterDataRecieved(GetUserDataResult result)
    {
        Debug.Log("recieved characters data!");
        if (result.Data != null && result.Data.ContainsKey("Characters"))
        {
            List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(result.Data["Characters"].Value);
            for (int i=0; i < characters.Count; i++)
            {
                GameObject UnitParent = Instantiate(Resources.Load("Prefabs/UI/UnitParent") as GameObject);
                UnitParent.transform.parent = Content.transform;
                //Debug.Log(UnitParent.transform.parent);
                UnitParent.SetActive(true);

                //Debug.Log("Images/" + characters[i].name + "_profile");
                UnitParent.GetComponent<GridViewItem>().SetGridUI(characters[i].name,"Lv." + characters[i].level.ToString(),Resources.Load<Sprite>("Images/"+characters[i].name + "_profile"));


                //Debug.Log(characters[i].stats.attackRange);
            }
        }
    }
    void OnError(PlayFabError error)
    {
        //Debug.Log("Error while logging in/creating account!");
        //Debug.Log(error.GenerateErrorReport());
        //messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
}
