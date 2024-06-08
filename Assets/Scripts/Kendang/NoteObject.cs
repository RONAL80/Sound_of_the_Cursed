using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public KeyCode[] redKeys = new KeyCode[2];
    public KeyCode[] blueKeys = new KeyCode[2];

    public bool canBePressed;
    private bool alreadyProcessed;
    private bool isRedNote;
    private bool isBlueNote;
    private bool isBigRedNote;
    private bool isBigBlueNote;

    private string triggerTag;

    void Start()
    {
        alreadyProcessed = false;
        isRedNote = gameObject.CompareTag("red_note");
        isBlueNote = gameObject.CompareTag("blue_note");
        isBigRedNote = gameObject.CompareTag("bigred_note");
        isBigBlueNote = gameObject.CompareTag("bigblue_note");
    }

    void Update()
    {
        if (Input.GetKeyDown(redKeys[0]) && Input.GetKeyDown(redKeys[1]))
        {
            HandleKeyPress(isBigRedNote);
        }
        else if (Input.GetKeyDown(blueKeys[0]) && Input.GetKeyDown(blueKeys[1]))
        {
            HandleKeyPress(isBigBlueNote);
        }
        else if (Input.GetKeyDown(redKeys[0]) || Input.GetKeyDown(redKeys[1]))
        {
            HandleKeyPress(isRedNote);
        }
        else if (Input.GetKeyDown(blueKeys[0]) || Input.GetKeyDown(blueKeys[1]))
        {
            HandleKeyPress(isBlueNote);
        }

        if (alreadyProcessed)
        {
            Destroy(gameObject);
        }
    }

    private void HandleKeyPress(bool isNote)
    {
        if (canBePressed && !alreadyProcessed)
        {
            if (isNote)
            {
                Note(triggerTag);
            }
            else
            {
                GameManager.instance.NoteMiss();
                alreadyProcessed = true;
            }
        }
    }

    private void Note(string triggerTag)
    {
        alreadyProcessed = true;
        if (isRedNote || isBlueNote)
        {
            if (triggerTag == "Activator")
            {
                GameManager.instance.PerfectHit();
            }
            else if (triggerTag == "goodActivator")
            {
                GameManager.instance.GoodHit();
            }
        }
        if (isBigRedNote || isBigBlueNote)
        {
            if (triggerTag == "Activator")
            {
                GameManager.instance.PerfectHit();
            }
            else if (triggerTag == "goodActivator")
            {
                GameManager.instance.GoodHit();
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Activator") || col.CompareTag("goodActivator"))
        {
            canBePressed = true;
            triggerTag = col.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Activator"))
        {
            canBePressed = false;
            if (!alreadyProcessed)
            {
                if (isRedNote || isBigRedNote)
                {
                    GameManager.instance.NoteMiss();
                }
                else if (isBlueNote || isBigBlueNote)
                {
                    GameManager.instance.NoteMiss();
                }
                alreadyProcessed = true;
            }
        }
    }
}