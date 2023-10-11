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

    public TrajectoryDataObject trajectoryData;

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
            
            if (tokenCount >= 3)
            {
                GiveTrophy();
            }
        }
    }

    private bool CheckTokens()
    {
        return (gameData.tokensCollected >= 3);
       
    }
  
    void GiveTrophy()
    {
        trophyPrefab.SetActive(true); 
        PlayTrophySound();
        TrajectoryVisualizer trajectoryVisualizer = FindObjectOfType<TrajectoryVisualizer>();

    }

    void ShowTrajectory()
    {
        TrajectoryVisualizer trajectoryVisualizer = FindObjectOfType<TrajectoryVisualizer>();
        if (trajectoryVisualizer != null)
        {
            trajectoryVisualizer.VisualizeTrajectory();
            trajectoryVisualizer.ExportTrajectoryData(trajectoryData);
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
