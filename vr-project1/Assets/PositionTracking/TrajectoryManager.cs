using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VelUtils;
using System.IO;
using UnityEngine.UI;


public class TrajectoryManager : MonoBehaviour
{
    private struct TrajectoryData
    {
        public Vector3 position;
        public Vector3 upVector;
        public Vector3 forwardVector;
        public Vector3 velocity;
    }

    [SerializeField]
    Transform rigTransform; 

    public Button viewTrajectoryButton;

    private List<TrajectoryData> trajectoryList = new List<TrajectoryData>();
    private Vector3 lastPosition;
    private LineRenderer lineRenderer;
    private float maxVelocity = 0f;

    private void Start()
    {
        if (!rigTransform)
        {
            Debug.LogError("Rig Transform not assigned!");
            return;
        }
        lineRenderer.enabled = false;

        lastPosition = rigTransform.position;

        // Setup LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Standard"));
        lineRenderer.widthCurve = new AnimationCurve(new Keyframe(0, 0.05f), new Keyframe(1, 0.05f)); // Setting the width of the line
    }

    private void Update()
    {
        if (!rigTransform)
        {
            Debug.LogError("Rig Transform not assigned!");
            return;
        }

        Vector3 currentPosition = rigTransform.position;
        Vector3 currentVelocity = (currentPosition - lastPosition) / Time.deltaTime;

        if (currentVelocity.magnitude > maxVelocity)
        {
            maxVelocity = currentVelocity.magnitude;
        }

        TrajectoryData data = new TrajectoryData
        {
            position = currentPosition,
            upVector = rigTransform.up,
            forwardVector = rigTransform.forward,
            velocity = currentVelocity
        };

        trajectoryList.Add(data);
        lastPosition = currentPosition;

        UpdateTrajectoryVisualization();
    }

    private void UpdateTrajectoryVisualization()
    {
        lineRenderer.positionCount = trajectoryList.Count;

        for (int i = 0; i < trajectoryList.Count; i++)
        {
            lineRenderer.SetPosition(i, trajectoryList[i].position);

            float velocityMagnitude = trajectoryList[i].velocity.magnitude;
            Color lineColor = Color.Lerp(Color.green, Color.red, velocityMagnitude / maxVelocity);
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
        }
    }
    public void ShowTrajectory()
    {
        lineRenderer.enabled = true;
    }

      public void OnGameCompleted()
    {
        viewTrajectoryButton.gameObject.SetActive(true); 
        viewTrajectoryButton.onClick.AddListener(ShowTrajectory); 
    }

    public void ExportData()
    {
        string path = "Path_to_your_directory/trajectoryData.txt";
        StreamWriter writer = new StreamWriter(path, false); 

        foreach (TrajectoryData data in trajectoryList)
        {
            string line = $"{data.position.x},{data.position.y},{data.position.z}";
            writer.WriteLine(line);
        }

        writer.Close();
    }
}





