using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject[] Lines;
    public GameObject beatRef;
    public Dictionary<int, List<GameObject>> spawnedBeats = new Dictionary<int, List<GameObject>>();
    private KeyHandler keyHandler;
    public AudioClip musicSong;
    Dictionary<int, int> linePos = new Dictionary<int, int>()
    {
        {36, 0},
        {109, 1},
        {182, 2},
        {256, 3},
        {329, 4},
        {402, 5},
        {475, 6}
    };

    void Start()
    {
        keyHandler = GetComponent<KeyHandler>();
        Lines = keyHandler.Lines;
        proceedMusic();
        //StartCoroutine(playMusicSong());
    }

    void Update()
    {
        
    }

    void spawnBeats(string music)
    {
        string[] beatmusic = music.Split('|');        
        float posX = 7f;
        for (int i = 0; i < beatmusic.Length; i++)
        {
            string beatData = beatmusic[i];
            int lineNum = int.Parse(beatData.Split(',')[0]);
            posX += int.Parse(beatData.Split(',')[2]) * 0.6f;
            GameObject beat = Instantiate(beatRef);
            beat.name = beatData;
            beat.transform.position = new Vector3(posX, Lines[lineNum].transform.position.y, 0f);
            addSpawnedBeatsToDictonary(lineNum, beat);
            StartCoroutine(moveBeat(beat));
        }
    }

    void addSpawnedBeatsToDictonary(int key, GameObject beat)
    {
        if (!spawnedBeats.ContainsKey(key))
        {
            spawnedBeats[key] = new List<GameObject>();
        }
        spawnedBeats[key].Add(beat);
    }

    IEnumerator moveBeat(GameObject beats)
    {
        while (true && beats != null)
        {
            beats.transform.position -= new Vector3(2.8f * Time.deltaTime, 0f, 0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    
    void proceedMusic()
    {
        string musicBeat = this.transform.GetComponent<Musics>().selectedMusic();
        string[] lagu = musicBeat.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        string hasil = "";
        for (int i = 0;i < lagu.Length - 1; i++)
        {
            string[] parts;
            parts = lagu[i].Split(',');
            parts[0] = linePos[int.Parse(parts[0])].ToString();
            parts[1] = 1.ToString();


            string[] secParts;
            secParts = lagu[i + 1].Split(',');

            if(i != lagu.Length -1)
            {
                int hasesCounts = 0;
                hasesCounts = (int.Parse(secParts[2]) - int.Parse(parts[2])) / 180;
                parts[2] = hasesCounts.ToString();
            }
            else
            {
                parts[2] = 0.ToString();
            }


            if(i != lagu.Length - 2)
            {
                hasil += string.Join(',', parts) + '|';
            }
            else
            {
                hasil += string.Join(',', parts);
            }
            print(string.Join(',', parts));
        }
        spawnBeats(hasil);
    }

    IEnumerator playMusicSong()
    {
        yield return new WaitForSeconds(3f);
        GameObject audioObject = new GameObject($"Audio-{musicSong.name}");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioObject.transform.parent = GameObject.Find("AudioPlaying").transform;
        audioSource.clip = musicSong;
        audioSource.Play();
        yield return null;
    }
}
