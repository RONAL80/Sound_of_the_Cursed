using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BeatRef : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.name.Contains("Perfect"))
        {
            string[] NameParts = transform.parent.name.Split(',');
            string[] Score = NameParts[5].Split(':');
            Score[1] = "1";
            NameParts[5] = string.Join(':', Score);
            transform.parent.name = string.Join(',', NameParts);   
        }
        else if (target.name.Contains("Good"))
        {
            string[] NameParts = transform.parent.name.Split(',');
            string[] Score = NameParts[5].Split(':');
            Score[1] = "2";
            NameParts[5] = string.Join(':', Score);
            transform.parent.name = string.Join(',', NameParts);
        }
        else if (target.name.Contains("SelfDestroyer"))
        {
            string[] NameParts = transform.parent.name.Split(',');
            string endSign = NameParts[5].Split(':')[2];
            if(endSign == "1")
            {
                StartCoroutine(GameObject.Find("GamePlayUi").GetComponent<UtilytiScriptSaron>().startFadeIn());
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                GameObject.Find("HealthPoint").GetComponent<HealthPoint>().increaseByScoreType("2");
                Destroy(transform.parent.gameObject);
                var spawner = GameObject.Find("Pad").GetComponent<Spawner>();
                spawner.spawnedBeats[int.Parse(transform.parent.name.Split(',').First())].RemoveAt(0);
            }
            
        }
    }
}
