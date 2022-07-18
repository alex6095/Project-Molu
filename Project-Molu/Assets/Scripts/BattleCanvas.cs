using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCanvas : MonoBehaviour
{
    GameObject[] ally;
    GameObject[] enemy;
    GameObject HPBarPrefab;
    GameObject HPBarObject;

    // Start is called before the first frame update
    void Awake()
    {
        HPBarPrefab = Resources.Load("Prefabs/UI/HP_Bar") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BattleStarted()
    {
        ally = BattleManager.Instance.GetAlly();
        enemy = BattleManager.Instance.GetEnemy();

        for (int i = 0; i < ally.Length; i++)
        {
            HPBarObject = Instantiate(HPBarPrefab);
            HPBarObject.transform.parent = transform;
            HPBarObject.GetComponent<HPBarHandler>().targetCharacter = ally[i];
        }
        for (int i = 0; i < enemy.Length; i++)
        {
            HPBarObject = Instantiate(HPBarPrefab);
            HPBarObject.transform.parent = transform;
            HPBarObject.GetComponent<HPBarHandler>().targetCharacter = enemy[i];
        }
    }
}
