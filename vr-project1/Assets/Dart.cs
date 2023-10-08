using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Dart : MonoBehaviour
{
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
        
        Debug.Log("collision detected");
        DartStand db = collision.gameObject.GetComponent<DartStand>();
        if (db != null)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            this.GetComponent<Rigidbody>().useGravity = false;
        }
        
    }
}
