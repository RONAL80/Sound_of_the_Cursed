using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCredit : MonoBehaviour
{
    [SerializeField] GameObject Credits;
    [SerializeField] GameObject ButtonToCredits;
    void Start()
    {
        if (PlayerPrefs.HasKey("GameFinished") && PlayerPrefs.HasKey("PlayingAs"))
        {
            if(PlayerPrefs.GetString("GameFinished") == PlayerPrefs.GetString("PlayingAs"))
            {
                Credits.SetActive(true);
                ButtonToCredits.SetActive(true);
                PlayerPrefs.DeleteKey("GameFinished");
                PlayerPrefs.DeleteKey("PlayingAs");
            }
        }
    }

    public void ShowCredits()
    {
        Credits.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
