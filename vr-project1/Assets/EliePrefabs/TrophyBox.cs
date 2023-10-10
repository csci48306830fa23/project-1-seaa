using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyBox : MonoBehaviour
{
    public int tokenCount = 0;
    public GameData gameData;
    public GameObject tokenPrefab;
    public GameObject trophyPrize; 
    public Transform trophySpawnPoint; 
    private Vector3 spawnOffset = new Vector3(0, 1, 0);
    public Button depositButton;

    public AudioSource trophySoundSource;

    private void Start() 
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("Token"))
        {
            Destroy(other.gameObject);
            tokenCount++;

            if (tokenCount >= 2)
            {
                GiveTrophy();
            }
        }

    }

    private bool CheckTokens()
    {
        return (gameData.tokensCollected >= 2);
       
    }
  
    void GiveTrophy()
    {
        Instantiate(trophyPrize, trophySpawnPoint.position, Quaternion.identity);
        PlayTrophySound();
        TrajectoryManager trajectoryManager = FindObjectOfType<TrajectoryManager>();
        if (trajectoryManager != null)
        {
            trajectoryManager.ShowTrajectory();
        }
        else
        {
            Debug.LogError("TrajectoryManager instance not found in the scene.");
        }
    }

    void PlayTrophySound()
    {
        if (trophySoundSource && !trophySoundSource.isPlaying)
        {
            trophySoundSource.Play();
        }
    }
}
