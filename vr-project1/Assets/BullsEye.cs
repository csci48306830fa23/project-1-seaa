using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullsEye : MonoBehaviour
{
    [SerializeField]
    Token tokenPrefab;
    [SerializeField]
    BullsEyeText bullsEyeText;
    [SerializeField]
    Transform tokenSpawnPoint;

    Boolean tokenSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        Dart d = collision.gameObject.GetComponent<Dart>();
        if (d != null)
        {
            d.stick();
            d.hitSound();
            bullsEyeText.GetComponent<MeshRenderer>().enabled = true;

            if (!tokenSpawned)
            {
                tokenSpawned = true;
                Token token = GameObject.Instantiate(tokenPrefab);
                Rigidbody rb = token.GetComponent<Rigidbody>();
                rb.position = tokenSpawnPoint.position;
                rb.GetComponent<Rigidbody>().isKinematic = false;
                rb.GetComponent<Rigidbody>().useGravity = true;
                
            }
            /*
            GameObject soundInstance = Instantiate(dartHitSoundPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
            audioSource.Play();
            Destroy(soundInstance, audioSource.clip.length);
            */

        }

    }
}
