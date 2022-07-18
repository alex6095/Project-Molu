using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
    public GameObject winImage;
    public GameObject loseImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void IsWin(bool isWin)
    {
        GameObject[] survivor;
        if (isWin)
        {
            winImage.SetActive(true);
            loseImage.SetActive(false);
            survivor = BattleManager.Instance.GetAlly();
            for (int i = 0; i < survivor.Length; i++)
            {
                if (survivor[i].gameObject.activeSelf && !survivor[i].gameObject.GetComponent<Ally>().isRetire)
                {
                    Camera.main.transform.position = survivor[i].transform.position + survivor[i].transform.forward * 2;
                    Camera.main.transform.LookAt(survivor[i].transform);
                    Camera.main.transform.position = Camera.main.transform.position - Camera.main.transform.right + new Vector3(0, 0.5f, 0);
                    break;
                }
            }
        }
        else
        {
            winImage.SetActive(false);
            loseImage.SetActive(true);
            survivor = BattleManager.Instance.GetEnemy();
            for (int i = 0; i < survivor.Length; i++)
            {
                if (survivor[i].gameObject.activeSelf && !survivor[i].gameObject.GetComponent<Enemy>().isRetire)
                {
                    Camera.main.transform.position = survivor[i].transform.position + survivor[i].transform.forward * 2;
                    Camera.main.transform.LookAt(survivor[i].transform);
                    Camera.main.transform.position = Camera.main.transform.position - Camera.main.transform.right + new Vector3(0, 0.5f, 0);
                    break;
                }
            }
        }
    }
}
