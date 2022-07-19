using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class PlaceCharacterButton : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    GameObject characterPrefab;
    GameObject characterObject;

    GameObject blackCover;
    bool isActiveState;

    public int buttonIdx;

    public Image buttonImg;
    public TMP_Text rarity;
    public Image weapon;

    private int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        //PlayFabOrgManager.instance.Team[buttonIdx].name
        //characterPrefab = Resources.Load("Prefabs/Characters/Ally/Unity-chan") as GameObject;

        blackCover = transform.GetChild(3).gameObject;
        blackCover.SetActive(false);
        isActiveState = true;

        // buttonImg = gameObject.GetComponent<Image>();
        buttonImg.sprite = Resources.Load<Sprite>("Images/" + PlayFabOrgManager.instance.Team[buttonIdx].name + "_profile") as Sprite;
        weapon.sprite = Resources.Load<Sprite>("Images/" + PlayFabOrgManager.instance.Team[buttonIdx].weapon) as Sprite;
        rarity.text = PlayFabOrgManager.instance.Team[buttonIdx].rarity.ToString();

        if (PhotonManager.instance.playerNo == 1)
            layerMask = 1 << LayerMask.NameToLayer("Ally Place Range");
        else
            layerMask = 1 << LayerMask.NameToLayer("Enemy Place Range");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isActiveState)
        {
            //characterObject = Instantiate(characterPrefab);
            //characterObject.tag = "Ally";
            //int layerMask = 1 << LayerMask.NameToLayer("Ally Place Range");
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            //{
            //    characterObject.transform.position = hit.point;
            //}

            if (PhotonManager.instance.playerNo == 1)
            {
                Debug.Log("Ally_" + PlayFabOrgManager.instance.Team[buttonIdx].name);
                characterObject = PhotonNetwork.Instantiate("Ally_" + PlayFabOrgManager.instance.Team[buttonIdx].name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                characterObject.tag = "Ally";
                
            }
            else
            {
                Debug.Log("Enemy_" + PlayFabOrgManager.instance.Team[buttonIdx].name);
                characterObject = PhotonNetwork.Instantiate("Enemy_" + PlayFabOrgManager.instance.Team[buttonIdx].name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                characterObject.tag = "Enemy";
            }
            // playerNo can not be 0

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                characterObject.transform.position = hit.point;
            }
            

            OnDrag(eventData);

            blackCover.SetActive(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isActiveState)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                characterObject.transform.position = hit.point;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isActiveState)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                characterObject.transform.position = hit.point;
            }

            isActiveState = false;
        }
    }
}
