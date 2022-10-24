using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeController : ProximityTrigger
{
    Vector3 InitialPosition;
    Vector3 InitialRotation;

    public GameObject Plunger;
    public Vector3 PlungerCollapsed;
    public Vector3 PlungerExpanded;

    public Material SyringeFilled;
    public Material SyringeEmpty;
    public MeshRenderer SyringeFillMesh;


    public void Start()
    {
        Targets = new GameObject[2];
        Targets[0] = GameController.Current.Vial;
        Targets[1] = GameController.Current.PatientArm;
        InitialPosition = transform.position;
        InitialRotation = transform.eulerAngles;
        GameController.Current.delegateResetGame += ResetSyringe;
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

            SyringeFilledUp();
        }
        if( Target == GameController.Current.PatientArm)
        {
            transform.parent = Target.transform;
            transform.position = Target.transform.position;

            GameController.Current.PlaceSyringeInPatient();

            SyringeEmptied();
        }
    }
    public void SyringePickedUP()
    {
        GameController.Current.PickUpSyringe();
    }
    public void ResetSyringe()
    {
        transform.position = InitialPosition;
        transform.eulerAngles = InitialRotation;

        SyringeEmptied();
    }
    public void SyringeFilledUp()
    {
        Plunger.transform.localPosition = PlungerExpanded;
        SyringeFillMesh.material = SyringeFilled;
    }
    public void SyringeEmptied()
    {
        Plunger.transform.localPosition = PlungerCollapsed;
        SyringeFillMesh.material = SyringeEmpty;
    }
}
