using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ProximityTrigger : MonoBehaviour
{
    public GameObject[] Targets;
    public float MinDistanceForTrigger;
    public void CheckProximity()
    {
        foreach(GameObject target in Targets)
        {
            if((transform.position - target.transform.position).magnitude <= MinDistanceForTrigger)
            {
                Action(target);
            }
        }
    }
    public abstract void Action(GameObject Target);
}
