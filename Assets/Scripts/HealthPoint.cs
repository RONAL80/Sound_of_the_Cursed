using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoint : MonoBehaviour
{
    float amountOfReduce = 0.01f;
    public TextMeshProUGUI Health;
    public Image HealthObject;

    void Start()
    {
        HealthObject = this.GetComponent<Image>();
        //StartCoroutine("selfHealthReducer");
    }

    void Update()
    {
        // Convert the fillAmount to a percentage and then to an integer
        int healthPercentage = Mathf.RoundToInt(HealthObject.fillAmount * 100);
        Health.text = healthPercentage.ToString();

    }

    public IEnumerator selfHealthReducer()
    {
        yield return new WaitForSeconds(1f);
        while (HealthObject.fillAmount > 0.00f)
        {
            HealthObject.fillAmount -= amountOfReduce;
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    public void reduceMissBeatHit()
    {
        HealthObject.fillAmount -= 0.02f;
    }

    public void increaseByScoreType(string scoreType)
    {
        if (scoreType != null && scoreType == "Good")
        {
            HealthObject.fillAmount += 0.01f;
        }
        else if (scoreType != null && scoreType == "Perfect")
        {
            HealthObject.fillAmount += 0.03f;
        }
        else if (scoreType != null && scoreType == "Miss")
        {
            HealthObject.fillAmount -= 0.01f;
        }
    }
}
