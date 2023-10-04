using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using VelUtils;

public class TeleportToGame : MonoBehaviour
{
    [SerializeField] Rig rig;
    [SerializeField] Transform spawn;

    public void Teleport()
    {
        Movement m = rig.GetComponent<Movement>();
        m.TeleportTo(spawn.position, spawn.forward);
    }
}
