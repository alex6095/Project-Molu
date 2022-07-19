using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class SkillButton2 : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    
    GameObject skillObject;

    public int buttonIdx;

    public Image buttonImg;
    public TMP_Text rarity;
    public Image weapon;

    private int layerMask;

    GameObject blackCover;
    GameObject black;
    public float coolTime;
    float leftTime;

    // Start is called before the first frame update
    void Start()
    {
        //PlayFabOrgManager.instance.Team[buttonIdx].name
        //characterPrefab = Resources.Load("Prefabs/Characters/Ally/Unity-chan") as GameObject;

        blackCover = transform.GetChild(3).gameObject;
        black = blackCover.transform.GetChild(0).gameObject;
        coolTime = 3.0f;
        leftTime = 0.0f;

        blackCover.SetActive(false);

        // buttonImg = gameObject.GetComponent<Image>();
        buttonImg.sprite = Resources.Load<Sprite>("Images/" + PlayFabOrgManager.instance.Team[buttonIdx].name + "_profile") as Sprite;
        weapon.sprite = Resources.Load<Sprite>("Images/" + PlayFabOrgManager.instance.Team[buttonIdx].weapon) as Sprite;
        rarity.text = PlayFabOrgManager.instance.Team[buttonIdx].rarity.ToString();

        //if (PhotonManager.instance.playerNo == 1)
        //    layerMask = 1 << LayerMask.NameToLayer("Ally Place Range");
        //else
        //    layerMask = 1 << LayerMask.NameToLayer("Enemy Place Range");

        layerMask = 1 << LayerMask.NameToLayer("Ground");
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (leftTime <= 0.0f)
        {
            //if (PhotonManager.instance.playerNo == 1)

            skillObject = PhotonNetwork.Instantiate("Skill_Sample", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

            OnDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (leftTime <= 0.0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                skillObject.transform.position = hit.point;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (leftTime <= 0.0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                skillObject.transform.position = hit.point;

                blackCover.SetActive(true);
                StartCoroutine(CoolTime(coolTime));

                skillObject.GetComponent<SkillObject>().Activate();
                PhotonNetwork.Destroy(skillObject);
            }

        }
    }

    IEnumerator CoolTime(float time)
    {
        leftTime = time;
        while (leftTime > 0.0f)
        {
            leftTime -= Time.deltaTime;
            blackCover.GetComponent<Image>().fillAmount = leftTime / coolTime;
            yield return new WaitForFixedUpdate();
        }
        blackCover.SetActive(false);
    }
}
