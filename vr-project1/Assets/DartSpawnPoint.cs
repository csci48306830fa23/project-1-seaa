using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartSpawnPoint : MonoBehaviour
{
    [SerializeField]
    Dart dartPrefab;
    [SerializeField]
    Transform dartSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        /*
        Dart dart = GameObject.Instantiate(dartPrefab);
        Rigidbody rb = dart.GetComponent<Rigidbody>();
        rb.position = dartSpawnPoint.transform.position;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
