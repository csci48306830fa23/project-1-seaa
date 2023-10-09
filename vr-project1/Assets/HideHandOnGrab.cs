using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VelUtils.VRInteraction;

public class HideHandOnGrab : MonoBehaviour
{

    [SerializeField]
    VRGrabbableHand hand;
    [SerializeField]
    GameObject handSphere;
    // Start is called before the first frame update
    void Start()
    {
        hand.OnGrab += (grabbable) =>
        {
            handSphere.GetComponent<MeshRenderer>().enabled = false;
        };
        hand.OnRelease += (grabbable) =>
        {
            handSphere.GetComponent<MeshRenderer>().enabled = true;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
