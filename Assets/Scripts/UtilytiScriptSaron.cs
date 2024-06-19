using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UtilytiScriptSaron : MonoBehaviour
{

    public GameObject HideSaronOnStart; 
    public GameObject Player;
    public GameObject Enemy;
    public GameObject StartCounter;
    public HealthPoint healthPoint;
    public GameObject DeathScreen;
    public GameObject PauseScreen;
    public CanvasGroup screenFade;
    public HealthPoint HealthPoint;
    bool isPaused = false;
    public SpriteRenderer CounterSign;
    public List<Sprite> CounterSprites;
    public GameObject LogoGameStart;
    private int timer = 3;
    public GameObject Pad;
    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("GAMESTART", "FALSE");
        initializeEnemyByLevel();
        StartCoroutine(startcount());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            RestartGame();
            PauseGame();
        }
        if (HideSaronOnStart != null && PlayerPrefs.GetString("GAMESTART") == "TRUE")
        {
            hideSaronOnStart();
        }
        
        if(healthPoint.HealthObject.fillAmount <= 0) {
            DeathScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else if(isPaused != true){
            DeathScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void initializeEnemyByLevel()
    {
        foreach(GameObject enm in enemies)
        {
            enm.SetActive(false);
        }
        enemies.ElementAt(PlayerPrefs.GetInt($"LevelGame{PlayerPrefs.GetString("PlayingAs")}")).SetActive(true);
        GameObject.Find("enemyName").GetComponent<TextMeshProUGUI>().text = enemies.ElementAt(PlayerPrefs.GetInt($"LevelGame{PlayerPrefs.GetString("PlayingAs")}")).name;
    }
    private void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseScreen.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }
    public void resumeGame()
    {
        if(isPaused == true)
        {
            isPaused = !isPaused;
            PauseScreen.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }
    public void QuitGame()
    {
        isPaused = !isPaused;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


    private void hideSaronOnStart()
    {
        HideSaronOnStart.transform.position += new Vector3(0f, 2f * Time.deltaTime, 0f);
    }

    IEnumerator startcount()
    {
        while(timer > 0)
        {
            CounterSign.sprite = CounterSprites.FirstOrDefault(sprite => sprite.name.Contains(timer.ToString()));
            timer--;
            yield return new WaitForSeconds(1f);
        }
        Destroy(CounterSign);
        LogoGameStart.SetActive(true);
        StartCoroutine(startFadeOut());
        yield return null;
    }




    IEnumerator startFadeOut()
    {
        float elapsedTime = 0f;
        float startAlpha = screenFade.alpha; // Alpha awal
        float targetAlpha = 0f; // Alpha tujuan
        yield return new WaitForSeconds(2f);
        LogoGameStart.SetActive(false);
        while (elapsedTime < 2f) // Durasi perubahan alpha
        {
            // Menggunakan fungsi Lerp untuk mengubah alpha secara perlahan
            screenFade.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 2f);
            elapsedTime += Time.deltaTime; // Menambah waktu yang sudah berlalu
            yield return null; // Menunggu frame berikutnya
        }
        // Pastikan alpha mencapai targetAlpha
        screenFade.alpha = targetAlpha;
        PlayerPrefs.SetString("GAMESTART", "TRUE");
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(healthPoint.selfHealthReducer());
    }

    public IEnumerator startFadeIn()
    {
        float elapsedTime = 0f;
        float startAlpha = screenFade.alpha; // Alpha awal
        float targetAlpha = 1f; // Alpha tujuan
        while (elapsedTime < 2f) // Durasi perubahan alpha
        {
            screenFade.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 2f);
            elapsedTime += Time.deltaTime; // Menambah waktu yang sudah berlalu
            yield return null; // Menunggu frame berikutnya
        }
        screenFade.alpha = targetAlpha;
        PlayerPrefs.SetInt($"DialogPart{PlayerPrefs.GetString("PlayingAs")}", 1);
        SceneManager.LoadScene("GameDialogStart1");
    }
}
