using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyBox : MonoBehaviour
{
    public int tokenCount = 0;
    public GameObject trophyPrize; 
    public Transform trophySpawnPoint; 

    public AudioSource trophySoundSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Token"))
        {
            Destroy(other.gameObject);
            tokenCount++;

            if (tokenCount == 3)
            {
                GiveTrophy();
            }
        }
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
