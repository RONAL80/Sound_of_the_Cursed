using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karakter : MonoBehaviour
{
    public float speed = 10.0f;

    void Update()
    {
        // Menggerakkan karakter ke arah kanan
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 movement = new Vector3(1, 0, 0);
            transform.Translate(movement * speed * Time.deltaTime);
        }
        // Menggerakkan karakter ke arah kiri
        else if (Input.GetKey(KeyCode.A))
        {
            Vector3 movement = new Vector3(-1, 0, 0);
            transform.Translate(movement * speed * Time.deltaTime);
        }
    }
}
