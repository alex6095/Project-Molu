using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class GameOverCanvas : MonoBehaviourPunCallbacks
{
    public GameObject winImage;
    public GameObject loseImage;

    public GameObject resultTime;
    public TMP_Text clock;

    public Camera p1Cam;
    public Camera p2Cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PhotonNetwork.LeaveRoom();
            
        }
    }
    public override void OnLeftRoom()
    {

        BGMManager.instance.PlayMainBGM();
        SceneManager.LoadScene("Organization");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //base.OnLeftRoom();
    }

    public void IsWin(bool isWin)
    {
        if (BattleManager.Instance.mode == 1)
        {
            resultTime.SetActive(true);
            resultTime.GetComponent<TMP_Text>().text = clock.text;
        }

        Camera mainCam;

        if (PhotonManager.instance.playerNo == 1)
            mainCam = p1Cam;
        else
            mainCam = p2Cam;


        GameObject[] survivor;
        if (isWin)
        {
            if (PhotonManager.instance.playerNo == 1)
            {
                winImage.SetActive(true);
                loseImage.SetActive(false);
            }
            else
            {
                winImage.SetActive(false);
                loseImage.SetActive(true);
            }
            survivor = BattleManager.Instance.GetAlly();
            for (int i = 0; i < survivor.Length; i++)
            {
                if (survivor[i].gameObject.activeSelf && !survivor[i].gameObject.GetComponent<Ally>().isRetire)
                {
                    mainCam.transform.position = survivor[i].transform.position + survivor[i].transform.forward * 2;
                    mainCam.transform.LookAt(survivor[i].transform);
                    mainCam.transform.position = mainCam.transform.position - mainCam.transform.right + new Vector3(0, 0.5f, 0);
                    break;
                }
            }
        }
        else
        {
            if (PhotonManager.instance.playerNo == 1)
            {
                winImage.SetActive(false);
                loseImage.SetActive(true);
            }
            else
            {
                winImage.SetActive(true);
                loseImage.SetActive(false);
            }
            survivor = BattleManager.Instance.GetEnemy();
            for (int i = 0; i < survivor.Length; i++)
            {
                if (survivor[i].gameObject.activeSelf && !survivor[i].gameObject.GetComponent<Enemy>().isRetire)
                {
                    mainCam.transform.position = survivor[i].transform.position + survivor[i].transform.forward * 2;
                    mainCam.transform.LookAt(survivor[i].transform);
                    mainCam.transform.position = mainCam.transform.position - mainCam.transform.right + new Vector3(0, 0.5f, 0);
                    break;
                }
            }
        }
    }
}
