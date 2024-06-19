using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveGamesManager : MonoBehaviour
{
    public CanvasGroup screenFade;
    string NewPlayerName = "";
    public InputField InputPlayerName;
    public GameObject[] popups;
    List<string> SaveNames = new List<string>();
    List<string> SaveDatas = new List<string>();
    int SlotNumber;
    public GameObject[] SaveSlots;
    void Start()
    {
        LoadAllSaves();
    }

    public void LoadAllSaves()
    {
        string saves = PlayerPrefs.GetString("Saves");
        if (string.IsNullOrEmpty(saves))
        {
            PlayerPrefs.SetString("Saves", "");
        }
        else
        {
            SaveNames = saves.Split('|').ToList();
            foreach (string saveName in SaveNames)
            {
                string saveData = PlayerPrefs.GetString(saveName);
                SaveDatas.Add(saveData);
                foreach (GameObject saveslot in SaveSlots)
                {
                    if (saveslot.name == saveName)
                    {
                        TextMeshProUGUI[] TextData = saveslot.GetComponentsInChildren<TextMeshProUGUI>();
                        string[] saveDataParts = saveData.Split('|');
                        for (int i = 0; i < TextData.Length && i < saveDataParts.Length; i++)
                        {
                            TextData[i].text = saveDataParts[i];
                        }
                    }
                }
            }
        }
    }

    public void CreateSaveGame(string GameMode)
    {
        NewPlayerName = InputPlayerName.text;
        string saveData = $"{NewPlayerName}|Stage Completed: 0/5|Mode Selected: {GameMode}";
        string saveName = "PlayerSave" + SlotNumber.ToString();

        if (!SaveNames.Contains(saveName))
        {
            SaveNames.Add(saveName);
        }

        PlayerPrefs.SetString(saveName, saveData);
        PlayerPrefs.SetString("Saves", string.Join("|", SaveNames));
        SaveSlots[SlotNumber].GetComponentsInChildren<TextMeshProUGUI>()[0].text = saveData.Split('|')[0];
        SaveSlots[SlotNumber].GetComponentsInChildren<TextMeshProUGUI>()[1].text = saveData.Split('|')[1];
        SaveSlots[SlotNumber].GetComponentsInChildren<TextMeshProUGUI>()[2].text = saveData.Split('|')[2];
        InputPlayerName.text = "";
        StartCoroutine(EnterTheGame(saveName));
    }

    public void isOverride(int slotnum)
    {
        SlotNumber = slotnum;
        //print(SlotNumber);
        string saveName = "PlayerSave" + slotnum;

        if (SaveNames.Contains(saveName))
        {
            popups[0].SetActive(true);
        }
        else
        {
            popups[0].SetActive(false);
            popups[1].SetActive(true);
        }
    }

    IEnumerator EnterTheGame(string saveName)
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
        PlayerPrefs.SetString("PlayingAs", saveName);
        SceneManager.LoadScene("OpeningScene");
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
