using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartSpawn : MonoBehaviour
{
    [SerializeField]
    Dart dartPrefab;

    [SerializeField]
    Transform throwPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dart dart = GameObject.Instantiate(dartPrefab);
            Rigidbody rb = dart.GetComponent<Rigidbody>();
            rb.position = throwPoint.position;
            //float randomRotationX = Random.Range(0.0f, 45.0f);
            //float randomRotationY = Random.Range(0.0f, 0.0f);
            //float randomRotationZ = Random.Range(0.0f, 60.0f);
            Quaternion randomRotation = Quaternion.Euler(
                Random.Range(-30f, 30f),
                Random.Range(-30f, 30f),
                Random.Range(-30f, 30f));
            rb.rotation = randomRotation;
            Vector3 dartDirection = randomRotation * Vector3.right;
            rb.velocity = dartDirection * 5.0f;
            //rb.velocity = new Vector3(0, 0, 5);
            //rb.AddRelativeForce(rb.velocity);

        }
    }
}
