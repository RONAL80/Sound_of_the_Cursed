using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Karakter : MonoBehaviour
{
    public float speed = 10.0f;
    public int cek = 0;

    public GameObject pocong;
    public GameObject tuyul;
    public GameObject hantu;
    public GameObject gorila;
    public GameObject dukun;

    private List<Transform> targets;
    private int currentIndex = -1; 
    private bool isMoving = false;

    void Start()
    {
       
        targets = new List<Transform> { pocong.transform, tuyul.transform, hantu.transform, gorila.transform, dukun.transform };
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D) && !isMoving)
        {
            cek += 1;
            Debug.Log(cek);

           
            currentIndex = (currentIndex + 1) % targets.Count;
            StartCoroutine(MoveToPosition(targets[currentIndex].position));
        }else if(Input.GetKeyDown(KeyCode.A) && !isMoving){
             currentIndex = (currentIndex - 1) % targets.Count;
            StartCoroutine(MoveToPosition(targets[currentIndex].position));
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        isMoving = true;
        
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        
        transform.position = targetPosition;
        isMoving = false;
        SceneManager.LoadScene("GameDialogStart1");
    }
}
