using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    bool tokenWin = false;  
    [SerializeField]
    GameObject tokenPrefab;  

    [SerializeField]
    Transform position;
    
    void Start(){

    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bag")
        {
            Win();
        }
    }

    public void Win()
    {
        if (!tokenWin)
            {
                spawnToken();
                tokenWin = true;
            }
    }

    public void spawnToken()
    {
        if (tokenPrefab != null)
        {
            Instantiate(tokenPrefab, position.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Token Prefab not assigned in TokenSpawner!");
        }
    }
}
