using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cornhole : MonoBehaviour
{
    [SerializeField] GameObject bag;
    [SerializeField] Transform bagSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawnBag();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnBag()
    {
        GameObject.Instantiate<GameObject>(bag, bagSpawn);
    }
}
