using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrajectoryManager : MonoBehaviour
{
    [System.Serializable]
    public class TrajectoryData
    {
        public Vector3 position;
        public Vector3 upVector;
        public Vector3 forwardVector;
        public float timeStamp;
    }

    public List<TrajectoryData> trajectoryDataList = new List<TrajectoryData>();

    // Visualization
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    private void Update()
    {
        CollectData();
    }

    void CollectData()
    {
        TrajectoryData data = new TrajectoryData
        {
            position = transform.position,
            upVector = transform.up,
            forwardVector = transform.forward,
            timeStamp = Time.time
        };

        trajectoryDataList.Add(data);
    }

    public void ShowTrajectory()
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = trajectoryDataList.Count;

        for (int i = 0; i < trajectoryDataList.Count; i++)
        {
            lineRenderer.SetPosition(i, trajectoryDataList[i].position);
        }
    }
}
