using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    private bool pointsAdded = false;  
    public int pointsToAdd = 1;    
    public GameData gameData;

    public void OnGrab()
    {
        if (!pointsAdded)
        {
            gameData.tokensCollected++;
            pointsAdded = true;
        }
    }


}
