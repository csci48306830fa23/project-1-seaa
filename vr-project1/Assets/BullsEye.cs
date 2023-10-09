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
    [SerializeField]
    Dart dartPrefab1;
    [SerializeField]
    Dart dartPrefab2;
    [SerializeField]
    Dart dartPrefab3;

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
            d.GetComponent<Rigidbody>().isKinematic = true;
            d.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            d.GetComponent<Rigidbody>().useGravity = false;
            bullsEyeText.GetComponent<MeshRenderer>().enabled = true;
            d.GetComponent<Rigidbody>().isKinematic = true;
            d.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            d.GetComponent<Rigidbody>().useGravity = false;

            if (!tokenSpawned)
            {
                Token token = GameObject.Instantiate(tokenPrefab);
                Rigidbody rb = token.GetComponent<Rigidbody>();
                rb.position = tokenSpawnPoint.position;
                rb.GetComponent<Rigidbody>().isKinematic = false;
                rb.GetComponent<Rigidbody>().useGravity = true;
                tokenSpawned = true;
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
