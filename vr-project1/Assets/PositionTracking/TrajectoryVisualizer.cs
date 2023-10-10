using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryVisualizer : MonoBehaviour
{
    public TrajectoryDataObject trajectoryData;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
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
        }
    }
}
