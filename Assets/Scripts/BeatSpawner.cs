using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BeatSpawner : MonoBehaviour
{
    public KeyBinds KeyBinds;
    public Musics musics;
    public Lines Lines;
    List<Transform> lines = new List<Transform>();
    float LineSpeed = 60f;
    float BeatSpawnSpeed = 0f;
    [SerializeField]
    string LineToSpeedUp = "";
    public string[] BeatColors =
    {
        "white",
        "magenta",
        "white",
        "red",
        "white",
        "magenta",
        "white"
    };
    void Start()
    {        
        lines = Lines.lines;
    }
    
    // Update is called once per frame
    void Update()
    {
    }
    
    public IEnumerator SpawnBeatsWithDelay()
    {
        bool simultaneousMode = false;
        bool holdBeat = false;
        int holdTime = 0;
        string simultaneousNotes = "";
        float delay = 0;
        BeatSpawnSpeed = 0.4f;
        foreach (char beat in musics.Beats)
        {
            if (beat == '#')
            {
                yield return new WaitForSeconds(BeatSpawnSpeed - 0.05f);
                continue;
            }
            if (beat == '(')
            {
                simultaneousMode = true;
                continue;
            }

            if (beat == ')')
            {
                simultaneousMode = false;
                if (!string.IsNullOrEmpty(simultaneousNotes))
                {
                    if (char.IsDigit(simultaneousNotes[0]))
                    {
                        for (int i = 0; i < simultaneousNotes.Length; i++)
                        {
                            char note = simultaneousNotes[i];
                            if (char.IsDigit(note))
                            {
                                holdTime = int.Parse(note.ToString());
                            }
                            else if (char.IsLetter(note))
                            {
                                foreach (Transform selectedLine in lines)
                                {
                                    if (selectedLine.name.Last() == note)
                                    {
                                        SpawnBeat(selectedLine, true, holdTime);
                                        delay = 0;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Transform selectedLine in lines)
                        {
                            foreach (char note in simultaneousNotes)
                            {
                                if (char.IsLetter(note))
                                {
                                    foreach (Transform selectedline in lines)
                                    {
                                        if (selectedline.name.Last() == note)
                                        {
                                            SpawnBeat(selectedline, false, 1);
                                            yield return new WaitForSeconds(BeatSpawnSpeed);
                                            delay = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                simultaneousNotes = ""; 
                yield return new WaitForSeconds(BeatSpawnSpeed);
                delay = 0;
                continue;
            }
            if (simultaneousMode)
            {
                simultaneousNotes += beat;
            }
            else
            {
                if (char.IsDigit(beat))
                {
                    holdBeat = true;
                    holdTime = int.Parse(beat.ToString());
                }
                else if (char.IsLetter(beat))
                {
                    foreach (Transform selectedLine in lines)
                    {
                        if (selectedLine.name.Last() == beat)
                        {
                            SpawnBeat(selectedLine, holdBeat, holdTime);
                            yield return new WaitForSeconds(BeatSpawnSpeed);
                            delay = 0;
                            holdBeat = false;
                            break; 
                        }
                    }
                }
            }
        }

    }

    public void SpawnBeat(Transform line, bool isHold, int holdTime)
    {
        string beatTospawn;
        GameObject beat;
        if (isHold)
        {
            beatTospawn = "HoldBeat";
            beat = Instantiate(GameObject.Find(beatTospawn));
            beat.name = "Hold-" + line.GetComponent<Line>().beatIteration + "-Beat-" + line.name.ToCharArray().Last();
            beat.transform.localScale = new Vector3(holdTime * 0.4f, 0.4f, 0f);
            beat.transform.parent = line;
            beat.transform.position = new Vector3(line.GetComponent<Collider2D>().bounds.max.x + beat.transform.lossyScale.x / 2, line.position.y, 0f);
            int index = KeyBinds.Keys.IndexOf(beat.name.Last());
            Color beatColor;
            ColorUtility.TryParseHtmlString(BeatColors[index], out beatColor);
            beat.transform.GetComponent<SpriteRenderer>().color = beatColor;
        }
        else
        {
            beatTospawn = "Beat";
            beat = Instantiate(GameObject.Find(beatTospawn));
            beat.name = line.GetComponent<Line>().beatIteration + "-Beat-" + line.name.ToCharArray().Last();
            beat.transform.localScale = new Vector3(0.46f, 0.4f, 0f);
            beat.transform.position = new Vector3(line.GetComponent<Collider2D>().bounds.max.x + beat.transform.lossyScale.x / 2, line.position.y, 0f);
            beat.transform.parent = line;
            int index = KeyBinds.Keys.IndexOf(beat.name.Last());
            Color beatColor;
            ColorUtility.TryParseHtmlString(BeatColors[index], out beatColor);
            beat.transform.GetComponent<SpriteRenderer>().color = beatColor;
        }

        Vector3 endPos = new Vector3(line.GetComponent<Collider2D>().bounds.min.x - line.transform.localScale.x / 4, line.transform.position.y, line.transform.position.z);
        line.GetComponent<Line>().Beats.Add(beat);
        line.GetComponent<Line>().beatIteration++;
        StartCoroutine(MoveBeatToEnd(beat.transform, endPos, LineToSpeedUp, LineSpeed));
    }

    public IEnumerator MoveBeatToEnd(Transform Beat, Vector3 EndPos, string toSpeedUp, float LineSpeed)
    {
            if (toSpeedUp != "" && toSpeedUp.Contains(Beat.name.Last()))
            {
                LineSpeed = LineSpeed * 2.5f;
            }
            float Speed = LineSpeed / 2f / 20f;
        while (true)
        {
            if (Beat != null && !Beat.name.Contains("ToHold-"))
            {
                Beat.position -= new Vector3(Speed * Time.deltaTime, 0f, 0f);
            }
            else if (Beat != null && Beat.name.Contains("ToHold-"))
            {
                Beat.localScale -= new Vector3((Speed * Time.deltaTime) * 0.307f, 0f, 0f);
                Beat.localPosition -= new Vector3((Speed * Time.deltaTime) * 0.314f, 0f, 0f) * 0.5f;
                yield return new WaitForSeconds(Time.deltaTime);
                if (Beat != null && Beat.localScale.x <= 0f)
                {
                    string scoreType = Beat.name.Split('-')[3];
                    Lines.multiplier.setMultiplier(scoreType);
                    Lines.hp.increaseByScoreType(scoreType);
                    Destroy(Beat.gameObject);
                    Beat.GetComponentInParent<Line>().Beats.Remove(Beat.gameObject);
                }
            }
            yield return null;
        }

    }
    

    
}
