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
    private string ppp = "\"Anyway, better start moving\",\r\n        \"After a few minutes walking the man find a cemetery. The man look inside and found a person wearing all white thus the man approach that person.\",\r\n        \"Good afternoon Mister, what a lovely day today. I saw you standing here alone and I decided to come here. Are you paying a visit sir?\",\r\n        \"It is indeed a lovely day young man.\",\r\n        \"Birds are singing.\",\r\n        \"Flowers are blooming.\",\r\n        \"On day like this young man like you…\",\r\n        \"State you business here young man.\",\r\n        \"Well, I just want to ask the villagers whereabout\",\r\n        \"I see so you are the person that he mention\",\r\n        \"What? you already know me?\",\r\n        \"And I also know why you are here young man.\",\r\n        \"Am I that popular?\",\r\n        \"Wait,,, who are you?\",\r\n        \"Some things are better left unsaid young man\",";
    public List<GameObject> dialogueBoxs;
    public TextMeshProUGUI dialogText;
    List<string> dialogues = new List<string>()
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
    };
    public int dialognum = 0;
    

    public CanvasGroup screenFade;
    void Start()
    {
        StartCoroutine(startFade());
    }

    public void nextDialog()
    {
        print(dialognum);
        if (dialognum != dialogues.Count)
        {
            dialognum++;
        }
        if(dialognum == dialogues.Count) {
            StartCoroutine(toStageDialog());
        }
        changeDialog(dialognum);
        print(dialognum);
    }
    void changeDialog(int num)
    {
        int keyDialog;
        if(dialognum < dialogues.Count)
        {
            keyDialog = int.Parse(dialogues[num].Split('|').First());
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
            dialogText.text = dialogues[num].Split('|').Last();
        }

        
    }
    IEnumerator toStageDialog()
    {
        yield return new WaitForSeconds(1f); // Menunggu selama 2 detik
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
        SceneManager.LoadScene("GameDialogStart1");
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
