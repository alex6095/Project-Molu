using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BattleStart()
    {
        BattleManager.Instance.BattleStart();
    }
}
