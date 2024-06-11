using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public HealthPoint hp;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision && collision.name.Contains("Beat"))
        {
            hp.reduceMissBeatHit();
            Destroy(collision.gameObject);
            collision.gameObject.transform.parent.GetComponent<Line>().Beats.Remove(collision.gameObject);
        }
    }
}
