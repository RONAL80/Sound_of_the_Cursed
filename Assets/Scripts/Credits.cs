using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject creditText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) {
            transform.gameObject.SetActive(false);
        }
        creditText.transform.localPosition += new Vector3(0f, 40 * Time.deltaTime, 0f); 
        if(creditText.transform.localPosition.y >= 1323f) {
            transform.gameObject.SetActive(false);
        }
        
    }
}
