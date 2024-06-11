using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malam : MonoBehaviour
{
    public GameObject malamku,siang;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        //malam ke pagi
         else if (Input.GetKey(KeyCode.W))
        {
           malamku.SetActive(false);
           siang.SetActive(true);
        }
    }
}
