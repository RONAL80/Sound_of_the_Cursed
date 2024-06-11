using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text Multiplier;
    public string currentMultiplier;
    public int strikeAmount = 0;
    int scorePoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseScore(string multiplier)
    {
        if(currentMultiplier == multiplier)
        {
            strikeAmount++;
        }
        else
        {
            currentMultiplier = multiplier;
            strikeAmount = 0;
        }

        if(strikeAmount <= 0)
        {
            Multiplier.text = currentMultiplier;
            scorePoint++;
            this.GetComponent<Text>().text = scorePoint.ToString();
        }
        else
        {
            scorePoint += (1 * strikeAmount);
            Multiplier.text = currentMultiplier+" "+strikeAmount+"X";
            this.GetComponent<Text>().text = scorePoint.ToString();
        }
        
    }
    public void decreaseScore()
    {
        if(scorePoint > 0 && scorePoint - 5 > 0 )
        {
            scorePoint -= 5;
            this.GetComponent<Text>().text = scorePoint.ToString();
        }
    }
}
