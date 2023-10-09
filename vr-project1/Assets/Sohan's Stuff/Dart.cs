using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using VelUtils.VRInteraction;

[RequireComponent(typeof(Rigidbody))]
public class Dart : MonoBehaviour
{
    [SerializeField]
    public VRGrabbableHand rightHand;
    [SerializeField]
    public VRGrabbableHand leftHand;
    int velocityMultiplier = 4;
    [SerializeField]
    Transform dartSpawnPoint;
    [SerializeField]
    Dart dartPrefab;
    [SerializeField]
    BullsEyeText bullsEyeText;
    //[SerializeField]
    
    // Start is called before the first frame update
    void Start()
    {
        rightHand = GameObject.Find("Right Hand").GetComponent<VRGrabbableHand>();
        leftHand = GameObject.Find("Left Hand").GetComponent<VRGrabbableHand>();
        bullsEyeText = GameObject.Find("BullsEye Text").GetComponent<BullsEyeText>();
        //dartSpawnPoint = GameObject.Find("DartSpawnPoint").GetComponent<Transform>();

        if (leftHand != null && rightHand != null)
        {
            leftHand.OnRelease += HandleDartRelease;
            rightHand.OnRelease += HandleDartRelease;
        }
    }

    private void OnDestroy()
    {
        if (leftHand != null && rightHand != null)
        {
            leftHand.OnRelease -= HandleDartRelease;
            rightHand.OnRelease -= HandleDartRelease;
        }
    }
    private void HandleDartRelease(VRGrabbable grabbedObject)
    {
        if (grabbedObject.gameObject == this.gameObject)  // Check if the released object is this Dart
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (rb.velocity != Vector3.zero)
                {
                    transform.forward = rb.velocity.normalized;
                }
                rb.angularVelocity = Vector3.zero;  // Reset angular velocity to stop spinning
                rb.velocity = rb.velocity * velocityMultiplier; //Double the release velocity of the dart

            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BullsEye")
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            this.GetComponent<Rigidbody>().useGravity = false;
            bullsEyeText.GetComponent<MeshRenderer>().enabled = true;
            /*
            GameObject soundInstance = Instantiate(dartHitSoundPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
            audioSource.Play();
            Destroy(soundInstance, audioSource.clip.length);
            */

        }
        Debug.Log("collision detected");
        DartStand db = collision.gameObject.GetComponent<DartStand>();
        if (db != null)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            this.GetComponent<Rigidbody>().useGravity = false;
            /*
            GameObject soundInstance = Instantiate(dartHitSoundPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
            audioSource.Play();
            Destroy(soundInstance, audioSource.clip.length);
            */

        }

    }

    private void OnTriggerEnter(Collider other)
    {


        MissedDartTrigger mdt = other.attachedRigidbody?.GetComponent<MissedDartTrigger>();
        if (mdt != null)
        {
            /*
            GameObject.Destroy(gameObject);
            Dart dart = GameObject.Instantiate(dartPrefab);
            Rigidbody rb = dart.GetComponent<Rigidbody>();
            rb.position = dartSpawnPoint.transform.position;
            */
            this.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            this.transform.position = dartSpawnPoint.transform.position;
            
        }
    }
}
