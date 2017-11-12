using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {

    private float currencyIncreasePerSecond = 0.25f;

    public GameObject currencyView;
    public GameObject scoreView;

    private float currentCurrency = 1000;
    private float currentScore = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        currentCurrency += currencyIncreasePerSecond * Time.deltaTime;

        currencyView.GetComponent<Text>().text = "Currency: " + ((int)currentCurrency).ToString();
        scoreView.GetComponent<Text>().text = "Score: " + ((int)currentScore).ToString();
    }

    public void AddCurrency(float currency)
    {
        currentCurrency += currency;
    }

    public bool DecreaseCurrency(int currency)
    {
        if (currentCurrency >= currency)
        {
            currentCurrency -= currency;
            return true;
        }

        return false;
    }

    public void AddScore(float score)
    {
        currentScore += score;
    }

    public float getScore()
    {
        return currentScore;
    }
}
