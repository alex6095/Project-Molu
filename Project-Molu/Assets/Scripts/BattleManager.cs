using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{ 
    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static BattleManager instance = null;
    bool isBattlePhase;
    public GameObject[] ally;
    public GameObject[] enemy;
    public GameObject placeCanvas;
    public GameObject battleCanvas;
    public GameObject gameOverCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isBattlePhase = false;
        placeCanvas.SetActive(true);
        battleCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);

        
    }

    public static BattleManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public void BattleStart()
    {
        isBattlePhase = true;
        ally = GameObject.FindGameObjectsWithTag("Ally");
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        placeCanvas.SetActive(false);
        battleCanvas.SetActive(true);
        battleCanvas.GetComponent<BattleCanvas>().BattleStarted();
        gameOverCanvas.SetActive(false);
    }

    public bool IsBattlePhase()
    {
        return isBattlePhase;
    }

    public GameObject[] GetAlly()
    {
        return ally;
    }

    public GameObject[] GetEnemy()
    {
        return enemy;
    }

    public void GameOver(bool isWin)
    {
        placeCanvas.SetActive(false);
        battleCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverCanvas.GetComponent<GameOverCanvas>().IsWin(isWin);
    }
}