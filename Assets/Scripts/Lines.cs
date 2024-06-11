using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Lines : MonoBehaviour
{
    public KeyBinds keyBinds;
    public List<Transform> lines = new List<Transform>();
    public Transform[] splashes;
    public HealthPoint hp;
    public Multiplier multiplier;
    public BeatSpawner beatSpawner;
    public SpriteRenderer ScoreSign;
    public List<Sprite> SpriteSigns;
    
    private string[] scoreTypes = {
        "Good",
        "Perfect",
        "Miss",
    };
    void Start()
    {
        initializeLinesChildern();
        initializeLinesSplashes();
        
    }
    void Update()
    {
        KeyPress();
    }
    
    public IEnumerator startSpawn()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(beatSpawner.SpawnBeatsWithDelay());
    }
    void initializeLinesChildern()
    {
        int indexLine = 0;
        foreach (Transform line in this.gameObject.GetComponentInChildren<Transform>())
        {
            lines.Add(line);
            line.name = line.name + "-" + keyBinds.Keys[indexLine];
            indexLine++;
        }
    }
    void initializeLinesSplashes()
    {
        splashes = GameObject.Find("LineSplash").GetComponentsInChildren<Transform>();
        for (int i = 0; i < keyBinds.Keys.Count; i++)
        {
            splashes[i + 1].name = splashes[i + 1].name + "-" + keyBinds.Keys[i];
        }
    }
    
    
    void KeyPress()
    {
        foreach (KeyCode keyCode in keyBinds.KeyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                keyBinds.KeyHoldStates[keyCode] = true;
                HandleKeyDown(keyCode);
            }
            else if (Input.GetKeyUp(keyCode))
            {
                keyBinds.KeyHoldStates[keyCode] = false;
                HandleKeyUp(keyCode);
            }
            else if (Input.GetKey(keyCode))
            {
                HandleKeyHold(keyCode);
            }
        }
    }

    public bool WasReleased = true;
    void HandleKeyDown(KeyCode keyCode)
    {
        Line line = this.transform.Find("Line-" + keyCode).GetComponent<Line>();
        line.gameObject.GetComponent<Line>().playAudio();
        //splashes.FirstOrDefault(splash => splash.name.EndsWith(keyCode.ToString())).GetComponent<SpriteRenderer>().enabled = true;

        if (line.Beats.Count > 0)
        {
            GameObject BEAT = line.Beats.First();
            bool isOnColider = scoreTypes.Any(st => BEAT.name.Contains(st));
            bool isHoldBeat = BEAT.name.Contains("Hold-") || BEAT.name.Contains("ToHold-");

            if (isOnColider && !isHoldBeat && WasReleased)
            {
                WasReleased = false;
                string scoreType = BEAT.name.Split('-')[1];
                scoreSignSet(scoreType);
                multiplier.setMultiplier(scoreType);
                hp.increaseByScoreType(scoreType);
                Destroy(line.Beats.First());
                line.Beats.RemoveAt(0); // Menghapus elemen pertama dari list
            }
            if (isOnColider && isHoldBeat && WasReleased)
            {
                string scoreType = BEAT.name.Split('-')[1]; // Ubah indeks ke 1 jika perlu
                BEAT.name = "ToHold-" + BEAT.name;
            }
            if (!isOnColider)
            {
                WasReleased = false;
                hp.reduceMissBeatHit();
                scoreSignSet("Miss");
            }
        }
        else
        {
            WasReleased = false;
            hp.reduceMissBeatHit();
            scoreSignSet("Miss");
        }
    }

    void HandleKeyHold(KeyCode keyCode)
    {
        
    }

    void HandleKeyUp(KeyCode keyCode)
    {
        WasReleased = true;
        Line line = this.transform.Find("Line-" + keyCode).GetComponent<Line>();
        //splashes.FirstOrDefault(splash => splash.name.EndsWith(keyCode.ToString())).GetComponent<SpriteRenderer>().enabled = false;

        if (line.Beats.Count > 0)
        {
            GameObject BEAT = line.GetComponentsInChildren<Transform>()[1].gameObject;
            string[] nameb = BEAT.name.Split('-');
            bool isHoldBeat = BEAT.name.Contains("ToHold-");
            print(BEAT.name);
            if (isHoldBeat)
            {
                BEAT.name = string.Join("-", nameb.Skip(1));
                print(BEAT.name);
                Destroy(BEAT.gameObject);
                line.Beats.Remove(BEAT);
            }
        }
    }

    void scoreSignSet(string sign)
    {
        if(sign != null)
        {
            Sprite spriteToSet = SpriteSigns.FirstOrDefault(sprite => sprite.name.Contains(sign));
            ScoreSign.sprite = spriteToSet;
        }
    }

}
