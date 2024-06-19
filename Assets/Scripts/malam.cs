using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malam : MonoBehaviour
{
    public GameObject malamku,siang;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("LevelDifficulty", 0);
    }
    //malam ke pagi
    

    // Update is called once per frame
    void Update()
    {
         //Pagi Ke  malam
        
        if (Input.GetKey(KeyCode.S))
        {
           malamku.SetActive(true);
           siang.SetActive(false);
            PlayerPrefs.SetInt($"LevelDifficulty{PlayerPrefs.GetString("PlayingAs")}", 1);
        }
        //malam ke pagi
         else if (Input.GetKey(KeyCode.W))
        {
           malamku.SetActive(false);
           siang.SetActive(true);
            PlayerPrefs.SetInt($"LevelDifficulty{PlayerPrefs.GetString("PlayingAs")}", 0);
        }
    }
}
