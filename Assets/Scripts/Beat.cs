using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Beat : MonoBehaviour
{
    Line LineParent;
    Collider2D beatCollider, 
        goodCollider, 
        perfectCollider;

    bool isOnPerfect = false,
        isOnGood = false;

    void Start()
    {
        LineParent = this.transform.GetComponentInParent<Line>();
        beatCollider = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Perfect"))
        {
            //LineParent.gameObject.GetComponent<Line>().playAudio();
        }
        if (this.gameObject && !this.name.Contains("Hold-") && !collision.name.Contains("HidingSaronOnStart") && !collision.name.Contains("Line-"))
        {
            int index = int.Parse(this.name.Split('-').First().ToString());
            LineParent.Beats.FindAll(beat => beat.name == this.name).First().name = index + $"-{collision.name}-Beat-" + this.name.Last();
        }
        if (this.gameObject && this.name.Contains("Hold-") && !collision.name.Contains("HidingSaronOnStart") && !collision.name.Contains("Line-"))
        {
            if (this.name.Contains("ToHold-"))
            {
                int index = int.Parse(this.name.Split('-')[2].ToString());
                LineParent.Beats.FindAll(beat => beat.name == this.name).First().name = "Hold-" + index + $"-{collision.name}-Beat-" + this.name.Last();
            }
            else
            {
                int index = int.Parse(this.name.Split('-')[1].ToString());
                LineParent.Beats.FindAll(beat => beat.name == this.name).First().name = "Hold-" + index + $"-{collision.name}-Beat-" + this.name.Last();
            }
        }
        
    }
}


