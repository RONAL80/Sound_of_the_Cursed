using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openingSceneScript : MonoBehaviour
{
    
    public List<GameObject> dialogueBoxs;
    public TextMeshProUGUI dialogText;
    [SerializeField] TextMeshProUGUI TargetName;
    int NumDialog;
    Dictionary<int, List<string>> Dialogues = new Dictionary<int, List<string>>()
    {
        {0,
            new List<string>()
            {
                "2|Once upon a time there was a man walking in the middle of rice field in the afternoon. The man is walking to his home when suddenly he got a phone call.",
                "2|*kriiing kriiing* The man picked up his phone from his pocket",
                "1|Hello? It’s me..",
                "0|Me who?",
                "1|Are you serious?",
                "0|No, I’m MC..",
                "1|Ugh, whatever, I have a job for you.",
                "0|Finally! After all these years I am no longer unemployed!",
                "1|Yes, I’m happy for you too, but please can you stay focus?",
                "0|Fine…",
                "1|So, your job is to purify a village from the evil within the village area and when you’re done we give you your payment.",
                "0|Sound simple enough.",
                "1|Yes, but this is a dangerous job, you will need all your skill and luck in order to finish it, therefore we wish you for the best. Good luck.",
                "1|Oh and don’t forget to pray to, because you need it.",
                "0|Wa Wait…",
                "2|*tuuut tuuut",
                "0|I guess I don’t have many choices ",
                "0|Better get home faster",
                "2|The man walk faster to his home and leaving the screen",
                "2|The man run towards his house, swiftly open the front door and locked it again. The man heart races as if it would jump out if his mouth. after catching his breath the man go to his room in there lay a white sheet. the man slowly try to unfold the sheet revealing the secret behind it. It was an instrument an ancient instrument that has become the man legacy for generation. The Saron",
                "0|Time to get to work pal",
                "2|The man arrived at the village entrance.",
                "0|Fyuh.. So this is the village huh?",
                "0|Hmmm… Strange, I don’t see any activities in the village",
                "0|Where all the villagers go?",
                "0|What was that sound?!",
                "0|Wait, that sound… is very familiar",
                "0|Is that you Wilhelm? ah i don't think so.",
                "2|The man continue walking and then...."
            }
        },
        {1,
            new List<string>()
            {
                "0|Pocong!",
                "0|How did this happend?!",
                "1|So is this the Player?",
                "0|What? Did I hear something?",
                "1|It seems that you can hear me Player.",
                "0|Who are you? Show yourself!",
                "1|There is no need to rush Player, your story has just begun.",
                "1|But if you want to know the truth remember this.",
                "1|’When the sun don’t shine.’",
                "0|What? What do you mean?",
                "2|silence",
                "2|.....",
                "2|.....",
                "0|Maybe I should keep going then."
            }
        },
        {2,
            new List<string>()
            {
                "1|And there goes another one good job Player you are one step ahead from the truth",
                "0|It’s you again!",
                "0|Tell me what happened to Tuyul and Pocong!",
                "1|Easy now Player, you’ll soon know the truth",
                "1|Oh, and remember this phrase",
                "1|And when all three stars are met",
                "0|What?",
                "0|What does that mean?",
                "2|.....",
                "2|.....",
                "0|Arghh!!",
                "0|This job is making me crazy!"
            }
        },
        {3,
            new List<string>()
            {
                "0|Isn’t it about time you show up?",
                "1|So you already know me here Player?",
                "0|Ha! I knew it!",
                "0|Don’t you have something to say now?",
                "1|Ha ha ha, patience is the key Player",
                "1|It is better late but come than fast but never come",
                "0|Ughh, just say it already",
                "1|Ha ha ha, fine…",
                "1|It seems like you already half way through the story Player",
                "1|Perhaps you already know the answer for this Player?",
                "1|Regardless, I will see you again next time",
                "0|Wait, at least tell me who you are?",
                "1|You will soon enough know me",
                "1|Bye",
                "1|For now"
            }
        },
    };

    public int dialognum = 0;
    

    public CanvasGroup screenFade;
    void Start()
    {
        NumDialog = PlayerPrefs.GetInt($"NumDialog{PlayerPrefs.GetString("PlayingAs")}");
        if(NumDialog == 0)
        {
            TargetName.text = "Mr PhoneCall";
        }
        else
        {
            if(NumDialog == 4)
            {
                SceneManager.LoadScene("StageSelect");
            }
            TargetName.text = "Unknown";
        }
        StartCoroutine(startFade());
    }

    public void nextDialog()
    {
        
        if (dialognum != Dialogues[NumDialog].Count -1)
        {
            dialognum++;
        }
        else
        {
            StartCoroutine(toStageDialog());
        }
        changeDialog(dialognum);
        print(dialognum);
    }
    void changeDialog(int num)
    {
        int keyDialog;
        if(dialognum < Dialogues[NumDialog].Count)
        {
            keyDialog = int.Parse(Dialogues[NumDialog][num].Split('|').First());
            if (keyDialog == 0)
            {
                dialogueBoxs[keyDialog].gameObject.SetActive(true);
                dialogText.transform.localPosition = new Vector3(-19.66f, -3.5f, 0);
            }
            else
            {
                dialogueBoxs[0].gameObject.SetActive(false);
            }

            if (keyDialog == 1)
            {
                dialogueBoxs[keyDialog].gameObject.SetActive(true);
                dialogText.transform.localPosition = new Vector3(-19.66f, -3.5f, 0);
            }
            else
            {
                dialogueBoxs[1].gameObject.SetActive(false);
            }
            if (keyDialog == 2)
            {
                dialogueBoxs[keyDialog].gameObject.SetActive(true);
                dialogText.transform.localPosition = new Vector3(-19.66f, 12.66f, 0);
            }
            else
            {
                dialogueBoxs[2].gameObject.SetActive(false);
            }
            dialogText.text = Dialogues[NumDialog][num].Split('|').Last();
        }

        
    }
    IEnumerator toStageDialog()
    {
        float elapsedTime = 0f;
        float startAlpha = screenFade.alpha; // Alpha awal
        float targetAlpha = 1f; // Alpha tujuan

        while (elapsedTime < 2f) // Durasi perubahan alpha
        {
            // Menggunakan fungsi Lerp untuk mengubah alpha secara perlahan
            screenFade.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 2f);
            elapsedTime += Time.deltaTime; // Menambah waktu yang sudah berlalu
            yield return null; // Menunggu frame berikutnya
        }

        // Pastikan alpha mencapai targetAlpha
        screenFade.alpha = targetAlpha;
        yield return new WaitForSeconds(1f);
        
        NumDialog++;
        PlayerPrefs.SetInt($"NumDialog{PlayerPrefs.GetString("PlayingAs")}", NumDialog);
        SceneManager.LoadScene("StageSelect");
        
    }

    IEnumerator startFade()
    {

        float elapsedTime = 0f;
        float startAlpha = screenFade.alpha; // Alpha awal
        float targetAlpha = 0f; // Alpha tujuan

        while (elapsedTime < 2f) // Durasi perubahan alpha
        {
            // Menggunakan fungsi Lerp untuk mengubah alpha secara perlahan
            screenFade.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 2f);
            elapsedTime += Time.deltaTime; // Menambah waktu yang sudah berlalu
            yield return null; // Menunggu frame berikutnya
        }

        // Pastikan alpha mencapai targetAlpha
        screenFade.alpha = targetAlpha;
        yield return new WaitForSeconds(0.5f);
        changeDialog(dialognum);
    }

}
