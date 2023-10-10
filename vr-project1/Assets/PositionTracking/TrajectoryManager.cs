using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VelUtils;


public class TrajectoryManager : MonoBehaviour
{
    private struct TrajectoryData
    {
        public Vector3 position;
        public Vector3 upVector;
        public Vector3 forwardVector;
        public Vector3 velocity;
    }

    private List<TrajectoryData> trajectoryList = new List<TrajectoryData>();
    private Vector3 lastPosition;
    private LineRenderer lineRenderer;

    private VRObject vrObject; 

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Standard"));
        
        vrObject = GetComponent<VRObject>();

        if (vrObject == null)
        {
            Debug.LogError("VRObject script not found on this GameObject.");
            return;
        }

        lastPosition = vrObject.transform.position;
    }

    private void Update()
    {
        Vector3 currentPosition = vrObject.transform.position;
        Vector3 currentVelocity = (currentPosition - lastPosition) / Time.deltaTime;

        TrajectoryData data = new TrajectoryData
        {
            position = currentPosition,
            upVector = vrObject.transform.up,
            forwardVector = vrObject.transform.forward,
            velocity = currentVelocity
        };

        trajectoryList.Add(data);
        lastPosition = currentPosition;  
        
    }
}


