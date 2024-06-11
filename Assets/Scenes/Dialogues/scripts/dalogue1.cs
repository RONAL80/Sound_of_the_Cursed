using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dalogue1 : MonoBehaviour
{
    public List<GameObject> characters;
    private List<float> charstopat = new List<float> { -285f, 300f};

    public List<GameObject> dialogueBoxs;
    public TextMeshProUGUI dialogText;
    List<string> dialogues = new List<string>()
    {
        "Ah.. there you go...",
        "Hihihi, trying to be a hero huh?",
        "Didn't mean it like that... but it looks like someone has to leave here soon or...",
        "Huh? Or what? ah, you're trying to play games with me...",
        "**Cihh, let see what's going to happen now...",
        "Look who's going to leave here...",
        "Now!! Let's start the show....",
    };
    public int dialognum = 0;
    List<int> dialogRoutes = new List<int>() {0,1,0,1,0,1,1};

    public CanvasGroup screenFade;
    void Start()
    {
        StartCoroutine(startFade());
    }

    IEnumerator playerSetMove()
    {
        float elapsedTime = 0f;
        Vector3 playerstartpos = characters[0].transform.localPosition;
        Vector3 enemystartpos = characters[1].transform.localPosition;

        while (elapsedTime < 3.0f)
        {
            characters[0].transform.localPosition = Vector3.Lerp(playerstartpos, new Vector3(charstopat[0], playerstartpos.y, playerstartpos.z), elapsedTime / 3.0f);
            characters[1].transform.localPosition = Vector3.Lerp(enemystartpos, new Vector3(charstopat[1], enemystartpos.y, enemystartpos.z), elapsedTime / 3.0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        characters[0].transform.localPosition = new Vector3(charstopat[0], playerstartpos.y, playerstartpos.z);
        dialogueBoxs[dialogRoutes[dialognum]].gameObject.SetActive(true);
        dialogText.text = dialogues[dialogRoutes[dialognum]];
    }

    public void nextDialog()
    {
        if(dialognum != 6)
        {
            dialognum++;
        }
        if(dialognum == 6)
        {
            StartCoroutine(toGamePlay());
        }
        changeDialog(dialognum);
    }
    void changeDialog(int num)
    {
        if(dialogRoutes[num] == 0)
        {
            dialogueBoxs[dialogRoutes[dialognum]].gameObject.SetActive(true);
        }
        else
        {
            dialogueBoxs[dialogRoutes[0]].gameObject.SetActive(false);
        }

        if(dialogRoutes[num] == 1)
        {
            dialogueBoxs[dialogRoutes[dialognum]].gameObject.SetActive(true);
        }
        else
        {
            dialogueBoxs[dialogRoutes[1]].gameObject.SetActive(false);
        }

        dialogText.text = dialogues[num];
    }
    IEnumerator toGamePlay()
    {
        yield return new WaitForSeconds(2f); // Menunggu selama 2 detik
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
        SceneManager.LoadScene("GamePlayScene");
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
        StartCoroutine(playerSetMove());
    }

}
