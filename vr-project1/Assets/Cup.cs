using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VelUtils.VRInteraction;

public class Cup : MonoBehaviour
{

    [SerializeField]
    ShellGame main;
    
    public int cupNum = -1;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<VRMoveable>().Grabbed += () =>
        {
            if (main.curBallPos == cupNum)
            {
                main.win();
            } else
            {
                main.loss(cupNum);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
