using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeatRef : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playNoteAudio(int lineNum)
    {
        GameObject audioObject = new GameObject($"Audio-{lineNum}");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioObject.transform.parent = GameObject.Find("AudioPlaying").transform;
        audioSource.clip = audioClips[lineNum];
        audioSource.Play();
        Destroy(audioObject, 1f);
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

            //temporary
            //playNoteAudio(int.Parse(NameParts[0]));
            //transform.parent.

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
                GameObject.Find("HealthPoint").GetComponent<HealthPoint>().increaseByScoreType("0");
                Destroy(transform.parent.gameObject);
                var spawner = GameObject.Find("Pad").GetComponent<Spawner>();
                spawner.spawnedBeats[int.Parse(transform.parent.name.Split(',').First())].RemoveAt(0);
            }
            
        }
    }
}
