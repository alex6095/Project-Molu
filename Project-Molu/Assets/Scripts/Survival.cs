using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survival : MonoBehaviour
{
    GameObject characterPrefab;
    GameObject characterObject;

    float leftTime;

    // Start is called before the first frame update
    void Start()
    {
        characterPrefab = Resources.Load("Prefabs/Characters/Ally/Unity-chan") as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.Instance.IsBattlePhase())
        {

        }
    }

    IEnumerator CoolTime(float time)
    {
        leftTime = time;
        while (leftTime > 0.0f)
        {
            leftTime -= Time.deltaTime;
            
            yield return new WaitForFixedUpdate();
        }
        
    }
}
