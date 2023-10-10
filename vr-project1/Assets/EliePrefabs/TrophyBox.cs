using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyBox : MonoBehaviour
{
    public int tokenCount = 0;
    public GameObject player;
    public GameObject tokenPrefab;
    public GameObject trophyPrize; 
    public Transform trophySpawnPoint; 
    public int coinsToSpawn = 3;
    public Vector3 spawnOffset = new Vector3(0, 1, 0);
    public Button depositButton;

    public AudioSource trophySoundSource;

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject == player)
        {
            depositButton.gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            depositButton.gameObject.SetActive(false);
        }
    }

    public void OnDepositButtonClicked()
    {
        Renderer trophyBoxRenderer = GetComponent<Renderer>();
        Vector3 topOfBox = trophyBoxRenderer.bounds.max;  
        Vector3 spawnPosition = topOfBox + new Vector3(0, 0.5f, 0);         
        
        for (int i = 0; i < coinsToSpawn; i++)
        {
            Vector3 offsetPosition = spawnPosition + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0, 0.2f), Random.Range(-0.1f, 0.1f));  // Randomizes the position a bit for each coin
             Instantiate(tokenPrefab, offsetPosition, Quaternion.identity);        
        }
        
        depositButton.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Token"))
        {
            Destroy(collision.gameObject);
            tokenCount++;

            if (tokenCount >= coinsToSpawn)
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
