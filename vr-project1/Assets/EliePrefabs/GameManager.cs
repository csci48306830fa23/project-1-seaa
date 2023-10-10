using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalPoints;
    public Text pointsText;

    private void Awake()
    {
       if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Debug.Log("GameManager instance set!");
            }
            else
            {
                Debug.LogWarning("Another instance of GameManager found! Destroying it.");
                Destroy(gameObject);
            }
        totalPoints = 0;
        UpdatePointsText();
    }

    public void AddPoints(int points)
    {
        totalPoints += points;
        UpdatePointsText();
        Debug.Log("Total Points: " + totalPoints);
    }

    void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + totalPoints;
        }
    }
}
