using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VelUtils.VRInteraction;
using DG.Tweening;
using VelUtils;
using System.Collections.Specialized;

public class Trophy : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    public Transform skyPosition;
    void Start()
    {
        
    }

    void Update()
    {
         this.GetComponent<VRMoveable>().Grabbed += () =>
        {
           TrajectoryVisualizer trajectoryVisualizer = FindObjectOfType<TrajectoryVisualizer>();
            if (trajectoryVisualizer != null)
            {
                trajectoryVisualizer.VisualizeTrajectory();
            }
            else
            {
                Debug.LogError("TrajectoryManager instance not found in the scene.");
            }
            player.transform.DOMove(skyPosition.position, 5f).SetEase(Ease.InOutSine);
        };
    }
}
