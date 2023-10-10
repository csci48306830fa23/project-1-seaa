using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera; 
    public Camera topDownCamera; 
    public float transitionDuration = 2.0f; 

    private bool isTransitioning = false;
    private float transitionTime = 0f;

    void Start()
    {
        mainCamera.enabled = true;
        topDownCamera.enabled = false;
    }

    public void SwitchToTopDownView()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            transitionTime = 0f;
        }
    }

    void Update()
    {
        if (isTransitioning)
        {
            transitionTime += Time.deltaTime / transitionDuration;

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, topDownCamera.transform.position, transitionTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, topDownCamera.transform.rotation, transitionTime);

            if (transitionTime >= 1.0f)
            {
                isTransitioning = false;
                mainCamera.enabled = false;
                topDownCamera.enabled = true;
            }
        }
    }
}

