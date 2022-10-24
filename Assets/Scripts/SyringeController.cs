using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeController : ProximityTrigger
{
    public void Start()
    {
        Targets = new GameObject[2];
        Targets[0] = GameController.Current.Vial;
        Targets[1] = GameController.Current.PatientArm;
    }
    public override void Action(GameObject Target)
    {
        InsertSyringe(Target);
    }
    public void InsertSyringe(GameObject Target)
    {
        if (Target == GameController.Current.Vial)
        {
            transform.parent = Target.transform;
            transform.position = Target.transform.position;

            GameController.Current.PlaceSyringeInVial();
        }
        if( Target == GameController.Current.PatientArm)
        {
            transform.parent = Target.transform;
            transform.position = Target.transform.position;

            GameController.Current.PlaceSyringeInPatient();
        }
    }
    public void SyringePickedUP()
    {
        GameController.Current.PickUpSyringe();
    }
}
