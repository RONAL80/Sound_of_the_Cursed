using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Line : MonoBehaviour
{
    public AudioClip audioClip;
    public List<GameObject> Beats = new List<GameObject>();
    public int beatIteration = 0;
    public int beatToDestroy = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playAudio()
    {
        GameObject audioObject = new GameObject("Audio-"+name.Last());
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioObject.transform.parent = GameObject.Find("AudioPlaying").transform;
        audioSource.clip = audioClip;
        audioSource.Play();
        Destroy(audioObject, audioClip.length);
    }
    
}
