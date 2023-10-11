using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryCollector : MonoBehaviour
{
    public TrajectoryDataObject trajectoryData;
    private LineRenderer lineRenderer;

    private void Start()
    {
        trajectoryData.ClearData();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        trajectoryData.AddData(transform.position, transform.up, transform.forward);
    }
}
