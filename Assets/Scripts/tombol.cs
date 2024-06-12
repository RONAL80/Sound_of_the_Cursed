using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tombol : MonoBehaviour
{
    public string pindahan;
    public CanvasGroup screenFade;
    bool isClicked = false;
    bool isContinueClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ContinueGamePlay()
    {
        if (isContinueClicked == false)
        {
            isContinueClicked = true;
            StartCoroutine(toGamePlay("GamePlayScene"));
        }
    }
    public void pindahScene(){
        if (isClicked == false) {
            isClicked = true;
            StartCoroutine(toGamePlay("NewGame"));
        }
    }

    IEnumerator toGamePlay(string scene)
    {
        float elapsedTime = 0f;
        float startAlpha = screenFade.alpha; // Alpha awal
        float targetAlpha = 1f; // Alpha tujuan

        while (elapsedTime < 1f) // Durasi perubahan alpha
        {
            // Menggunakan fungsi Lerp untuk mengubah alpha secara perlahan
            screenFade.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 1f);
            elapsedTime += Time.deltaTime; // Menambah waktu yang sudah berlalu
            yield return null; // Menunggu frame berikutnya
        }

        // Pastikan alpha mencapai targetAlpha
        screenFade.alpha = targetAlpha;
        SceneManager.LoadScene(scene);
        isClicked = false;
    }

    //create function to exit game
    public void ExitGame()
    {
        Application.Quit();
    }
}
