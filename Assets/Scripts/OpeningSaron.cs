using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSaron : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.name == "Opener") {
            GameObject.Find("Pad").GetComponent<Spawner>().enabled = true;
            Destroy(this.gameObject);
        }
    }
}
