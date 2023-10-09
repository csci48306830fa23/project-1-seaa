using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornholeScript : MonoBehaviour
{
    [SerializeField] GameObject bag;
    [SerializeField] Transform bagSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawnBag();
        spawnBag();
        spawnBag();
    }

    void spawnBag()
    {
        GameObject.Instantiate<GameObject>(bag, bagSpawn.position, Quaternion.identity);
    }
}
