using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTeleport : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject thePlayer;
    void OnTriggerEnter(Collider teleporter)
    {
        thePlayer.transform.position = teleportTarget.transform.position;
    }
}
