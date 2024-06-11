using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour
{
    Text multiplierText;
    public float beatsCounts;
    int perfectsCount;
    int goodCount;
    int multiplierStrikes;
    string currentMultiplier;
    void Start()
    {
        multiplierText = this.GetComponent<Text>();
        multiplierText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setMultiplier(string multiplier)
    {
        if (currentMultiplier == multiplier)
        {
            multiplierStrikes ++;
        }
        else
        {
            currentMultiplier = multiplier;
            multiplierStrikes = 0;
        }

        if (multiplier != null && multiplier == "Perfect")
        {
            perfectsCount ++;
            if(multiplierStrikes <= 0)
            {
                multiplierText.text = multiplier;
            }
            else
            {
                multiplierText.text = multiplier +" "+ multiplierStrikes+"X";
            }
        }else if(multiplier != null && multiplier == "Good")
        {
            goodCount++;
            if (multiplierStrikes <= 0)
            {
                multiplierText.text = multiplier;
            }
            else
            {
                multiplierText.text = multiplier + " " + multiplierStrikes + "X";
            }
        }else if( multiplier != null && multiplier == "Miss")
        {
            multiplierStrikes = 0;
            
            multiplierText.text = multiplier;
        }
    }
}
