using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalPoints = 0;
    public Text pointsText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

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
