using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainmenuManager : MonoBehaviour
{
    public void GoContentsButton()
    {
        SceneManager.LoadScene("Contents");
    }
    public void GoManagementButton()
    {
        SceneManager.LoadScene("Management");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
