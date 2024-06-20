using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSaron : MonoBehaviour
{
    [SerializeField] GameObject KeySigns;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.name == "Opener") {
            GameObject.Find("Pad").GetComponent<Spawner>().enabled = true;
            KeySigns.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
