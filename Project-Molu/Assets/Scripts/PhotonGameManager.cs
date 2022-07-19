using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PhotonGameManager : MonoBehaviour
{

    public Camera p1cam;
    public Camera p2cam;

    public TMP_Text playerNo;

    private void Awake()
    {
        if (PhotonManager.instance.playerNo == 1)
        {
            playerNo.text = "Player : " + PhotonManager.instance.playerNo;
            p1cam.gameObject.SetActive(true);
        }
        else
        {
            playerNo.text = "Player : " + PhotonManager.instance.playerNo;
            p2cam.gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
