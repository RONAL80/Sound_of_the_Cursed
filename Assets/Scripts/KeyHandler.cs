using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyHandler : MonoBehaviour
{
    private List<KeyCode> keyBinds = new List<KeyCode>() {KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.L};
    public Dictionary<KeyCode, bool> keyStates = new Dictionary<KeyCode, bool>();
    public List<KeyCode> keyInputs = new List<KeyCode>();
    public List<AudioClip> audioClips = new List<AudioClip>();
    private Spawner spawner;
    public GameObject[] Lines;
    public HealthPoint hp;
    public SpriteRenderer ScoreSign;
    public List<Sprite> SpriteSigns;

    private string[] scoreTypes = {
        "Good",
        "Perfect",
        "Miss",
    };

    void Start()
    {
        spawner = this.gameObject.GetComponent<Spawner>();
        initializeKeys();
        initializeLines();
    }
    void Update()
    {
        inputClickHandle();
    }
    void initializeKeys()
    {
        foreach (KeyCode keyCode in keyBinds)
        {
            keyInputs.Add(keyCode);
        }

        foreach (KeyCode keyInput in keyInputs)
        {
            keyStates[keyInput] = false;
        }
    }

    void initializeLines()
    {
        for(int i = 0; i <  Lines.Length; i++)
        {
            Lines[i].name = $"Line-{keyInputs[i]}";
        }
    }

    void inputClickHandle()
    {
        // Reset all lines to red at the beginning of each frame
        foreach (var line in Lines)
        {
            line.GetComponent<SpriteRenderer>().color = Color.red;
        }

        // Check key states and update colors
        foreach (KeyCode key in keyInputs)
        {
            keyStates[key] = Input.GetKeyDown(key);
            if (keyStates[key])
            {
                for (int i = 0; i < Lines.Length; i++)
                {
                    if (Lines[i].name == $"Line-{key}")
                    {
                        playNoteAudio(i);
                        GameObject.Find("mc").GetComponent<Animator>().Play("attack");
                        executeBeatEachKey(i);
                    }
                }
            }
        }
    }

    void executeBeatEachKey(int index)
    {
        if (spawner.spawnedBeats != null && spawner.spawnedBeats[index] != null)
        {
            string scoreType = spawner.spawnedBeats[index].First().name.Split(',')[5].Split(':')[1];
            if (scoreType != "0")
            {
                showScoreType(scoreType);
                hasFinishedTheGame(spawner.spawnedBeats[index].First().name.Split(',')[5].Split(':')[2]);
                Destroy(spawner.spawnedBeats[index].First().gameObject);
                spawner.spawnedBeats[index].RemoveAt(0);
            }
            else
            {
                showScoreType(scoreType);
            }
        }
        else
        {
            showScoreType("0");
        }
    }

    void showScoreType(string score)
    {
        if (score != null)
        {
            Sprite spriteToSet = SpriteSigns[int.Parse(score)];
            ScoreSign.sprite = spriteToSet;
            hp.increaseByScoreType(score);
        }
    }

    void playNoteAudio(int lineNum)
    {
        GameObject audioObject = new GameObject($"Audio-{lineNum}");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioObject.transform.parent = GameObject.Find("AudioPlaying").transform;
        audioSource.clip = audioClips[lineNum];
        audioSource.Play();
        Destroy(audioObject, 1f);
    }

    void hasFinishedTheGame(string sign)
    {
        if(sign != null && sign != "" && sign == "1")
        {
            StartCoroutine(GameObject.Find("GamePlayUi").GetComponent<UtilytiScriptSaron>().startFadeIn());
        }
    }
    
}
