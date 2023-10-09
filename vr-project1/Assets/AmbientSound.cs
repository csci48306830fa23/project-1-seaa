using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmbientSound : MonoBehaviour
{

    [SerializeField]
    AudioSource ambient;

    // Start is called before the first frame update
    void Start()
    {
        ambient.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
