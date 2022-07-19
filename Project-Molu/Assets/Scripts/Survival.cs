using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Survival : MonoBehaviour
{
    GameObject characterPrefab;
    GameObject characterObject;

    public GameObject spawnPosition;

    float clockTime;
    public TMP_Text clock;
    bool started;
    int enemyNum;

    // Start is called before the first frame update
    void Awake()
    {
        BattleManager.Instance.mode = 1;
        characterPrefab = Resources.Load("Prefabs/Characters/Enemy/Unity-chan") as GameObject;
        clockTime = 0;
        started = false;
        enemyNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.Instance.IsBattlePhase())
        {
            if (!started)
            {
                StartCoroutine(Spawn(5.0f));
                started = true;
            }
            clockTime += Time.deltaTime;
            clock.text = clockTime.ToString();
        }

    }

    IEnumerator Spawn(float time)
    {
        while (BattleManager.Instance.IsBattlePhase())
        {
            print(clockTime / enemyNum);
            if(clockTime / enemyNum > time)
            {
                characterObject = Instantiate(characterPrefab);
                characterObject.tag = "Enemy";
                characterObject.transform.position = spawnPosition.transform.position;
                BattleManager.Instance.MakeNewCharacter(characterObject);
                enemyNum += 1;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
