using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TrajectoryVisualizer : MonoBehaviour
{
    public TrajectoryDataObject trajectoryData;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        lineRenderer.enabled = false;
    }

    public void VisualizeTrajectory()
    {
        if (trajectoryData && lineRenderer)
        {
            int dataCount = trajectoryData.trajectoryDataList.Count;
            lineRenderer.positionCount = dataCount;

            for (int i = 0; i < dataCount; i++)
            {
                lineRenderer.SetPosition(i, trajectoryData.trajectoryDataList[i].position);
            }
            lineRenderer.enabled = true; 
        }
    }

    public void ExportTrajectoryData(TrajectoryDataObject trajectoryDataObject)
    {
        if (trajectoryData)
        {
            string jsonData = JsonUtility.ToJson(trajectoryDataObject, true);
            string path = Path.Combine(Application.persistentDataPath, "trajectoryData.json");
            File.WriteAllText(path, jsonData);

            Debug.Log("Data saved to: " + path);
        
        }  
    }
}
