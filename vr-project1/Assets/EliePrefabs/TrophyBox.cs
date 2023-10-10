using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyBox : MonoBehaviour
{
    public int tokenCount = 0;
    public GameData gameData;
    public GameObject tokenPrefab;
    public GameObject trophyPrefab;
    private Vector3 spawnOffset = new Vector3(0, 1, 0);
    public AudioSource trophySoundSource;

    public AudioClip coinSound;

    private void Start() 
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Token")
        {
            Destroy(collision.gameObject);
            tokenCount++;
            AudioSource.PlayClipAtPoint(coinSound, trophySoundSource.transform.position);
            
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
        Instantiate(trophyPrefab, this.transform.position+new Vector3(0,2,0), Quaternion.identity);
        PlayTrophySound();
        TrajectoryVisualizer trajectoryVisualizer = FindObjectOfType<TrajectoryVisualizer>();
        if (trajectoryVisualizer != null)
        {
            trajectoryVisualizer.VisualizeTrajectory();
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
