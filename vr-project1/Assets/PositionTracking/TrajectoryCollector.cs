using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryCollector : MonoBehaviour
{
    public TrajectoryDataObject trajectoryData;

    private void Start()
    {
        trajectoryData.ClearData();
    }

    private void Update()
    {
        trajectoryData.AddData(transform.position, transform.up, transform.forward);
    }
}
