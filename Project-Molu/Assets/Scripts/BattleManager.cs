using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleManager : MonoBehaviour
{ 
    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static BattleManager instance = null;
    bool isBattlePhase;
    bool isGameOver;
    public GameObject[] ally;
    public GameObject[] enemy;
    public GameObject placeCanvas;
    public GameObject battleCanvas;
    public GameObject gameOverCanvas;

    public int playerReady = 0;

    public int mode; // 모드 1은 서바이벌

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
        isGameOver = false;
        placeCanvas.SetActive(true);
        battleCanvas.SetActive(false);//
        gameOverCanvas.SetActive(false);


        BGMManager.instance.PlayBattleBGM();
    }

    void Update()
    {
        if(isBattlePhase && mode == 1)
        {
            bool isLose = true;
            for (int i = 0; i < ally.Length; i++)
            {
                if (ally[i].gameObject.activeSelf && !ally[i].gameObject.GetComponent<Ally>().isRetire)
                {
                    isLose = false;
                }
            }
            if (isLose)
            {
                GameOver(false);
            }
            if (ally.Length+enemy.Length == 0)
            {
                // 무승부 처리 후에 하기
                GameOver(true);
            }
        }

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
        
        battleCanvas.SetActive(true);
        battleCanvas.GetComponent<BattleCanvas>().BattleStarted();
        gameOverCanvas.SetActive(false);
    }

    public void MakeNewCharacter(GameObject character)
    {
        battleCanvas.GetComponent<BattleCanvas>().NewCharacterEntered(character);
        Array.Resize(ref enemy, enemy.Length+1);
        enemy[enemy.Length - 1] = character;
        print(enemy[enemy.Length - 1].name);
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
        for (int i = 0; i < ally.Length; i++)
        {
            if (ally[i].gameObject.activeSelf && !ally[i].gameObject.GetComponent<Ally>().isRetire)
            {
                ally[i].GetComponent<Animator>().SetBool("isEnd", true);
            }
        }
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i].gameObject.activeSelf && !enemy[i].gameObject.GetComponent<Enemy>().isRetire)
            {
                enemy[i].GetComponent<Animator>().SetBool("isEnd", true);
            }
        }

        isBattlePhase = false;
        isGameOver = true;
        placeCanvas.SetActive(false);
        battleCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);

        gameOverCanvas.GetComponent<GameOverCanvas>().IsWin(isWin);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}