using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrajectoryData", menuName = "Trajectory/Trajectory Data")]
public class TrajectoryDataObject : ScriptableObject
{
    [System.Serializable]
    public class TrajectoryData
    {
        public Vector3 position;
        public Vector3 upVector;
        public Vector3 forwardVector;
    }

    public List<TrajectoryData> trajectoryDataList = new List<TrajectoryData>();

    public void AddData(Vector3 position, Vector3 upVector, Vector3 forwardVector)
    {
        TrajectoryData data = new TrajectoryData
        {
            position = position,
            upVector = upVector,
            forwardVector = forwardVector
        };
        trajectoryDataList.Add(data);
    }

    public void ClearData()
    {
        trajectoryDataList.Clear();
    }
}
