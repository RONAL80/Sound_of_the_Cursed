using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Karakter : MonoBehaviour
{
    public float speed = 10.0f;
    public int cek = 0;

    public GameObject gorila;
    public GameObject pocong;
    public GameObject tuyul;
    public GameObject hantu;
    public GameObject dukun;

    private List<Transform> targets;
    private int currentIndex; 
    private bool isMoving = false;
    

    void Start()
    {
        if (PlayerPrefs.HasKey($"LastPosLevel{PlayerPrefs.GetString("PlayingAs")}") && PlayerPrefs.GetString($"LastPosLevel{PlayerPrefs.GetString("PlayingAs")}") != "")
        {
            currentIndex = PlayerPrefs.GetInt($"LevelGame{PlayerPrefs.GetString("PlayingAs")}");
            string[] LastPos = PlayerPrefs.GetString($"LastPosLevel{PlayerPrefs.GetString("PlayingAs")}").Split(":");
            this.transform.position = new Vector3(float.Parse(LastPos[0]), float.Parse(LastPos[1]), float.Parse(LastPos[2]));
        }
        else
        {
            currentIndex = -1;
        }
        targets = new List<Transform> {gorila.transform, tuyul.transform, hantu.transform, pocong.transform, dukun.transform };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && !isMoving)
        {
            cek += 1;
            currentIndex = (currentIndex + 1) % targets.Count;
            StartCoroutine(MoveToPosition(targets[currentIndex]));
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isMoving)
        {
            currentIndex = (currentIndex - 1 + targets.Count) % targets.Count; // Ensure wrap-around for negative indices
            StartCoroutine(MoveToPosition(targets[currentIndex]));
        }

        if (!isMoving && Input.GetKeyDown(KeyCode.Return))
        {
            if(currentIndex != -1)
            {
                PlayerPrefs.SetInt($"LevelGame{PlayerPrefs.GetString("PlayingAs")}", currentIndex);
                PlayerPrefs.SetString($"LastPosLevel{PlayerPrefs.GetString("PlayingAs")}", $"{targets[currentIndex].position.x}:{targets[currentIndex].position.y}:{targets[currentIndex].position.z}");
                PlayerPrefs.SetInt($"DialogPart{PlayerPrefs.GetString("PlayingAs")}", 0);
                setPlayerSaveData();
                SceneManager.LoadScene("GameDialogStart1");
            }
        }
    }

    void setPlayerSaveData()
    {
        print(PlayerPrefs.GetString(PlayerPrefs.GetString("PlayingAs")).Split("|")[1]);
        string[] saveDataParts = PlayerPrefs.GetString(PlayerPrefs.GetString("PlayingAs")).Split("|");
        saveDataParts[1] = $"Stage Completed: {currentIndex + 1}/5";
        PlayerPrefs.SetString(PlayerPrefs.GetString("PlayingAs"), string.Join('|', saveDataParts));
    }
    IEnumerator MoveToPosition(Transform target)
    {
        Vector3 targetPosition = target.position;
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition; // Ensure exact position match
        isMoving = false;
    }
}
