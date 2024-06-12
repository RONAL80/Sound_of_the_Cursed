using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public GameObject confirmExit;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            isPaused = true;
            confirmExit.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            isPaused = false;
            confirmExit.SetActive(false);
        }
        
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
