using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class OrganizationManager : MonoBehaviour
{
    //public void GoBattleButton()
    //{
    //    SceneManager.LoadScene("Battle");
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBackButton()
    {
        SceneManager.LoadScene("Contents");
    }
}
