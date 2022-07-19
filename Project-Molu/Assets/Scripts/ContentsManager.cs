using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ContentsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoPVPButton()
    {
        SceneManager.LoadScene("Organization");
    }
    public void GoSurvivalButton()
    {
        SceneManager.LoadScene("Survival");
    }
    public void GoBackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
