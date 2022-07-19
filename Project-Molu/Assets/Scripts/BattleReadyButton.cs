using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class BattleReadyButton : MonoBehaviour
{
    public GameObject placeCanvas;
    PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BattleReady()
    {
        placeCanvas.SetActive(false);
        addReadyPlayer();
        pv.RPC("addReadyPlayer", RpcTarget.Others);

        //Debug.Log(BattleManager.Instance.playerReady);
        
    }
    [PunRPC]
    void addReadyPlayer()
    {
        BattleManager.Instance.playerReady += 1;
        Debug.Log(BattleManager.Instance.playerReady);
        if (BattleManager.Instance.playerReady == 2)
        {
            BattleManager.Instance.BattleStart();
        }
    }
}
