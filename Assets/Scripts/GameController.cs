using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Current;

    private void Awake()
    {
        if (Current == null)
        {
            Current = this;
        }
    }

    public static bool LeftGloveOn = false;
    public static bool RightGloveOn = false;
    public static bool SyringePickedUp = false;
    public static bool SyringeInVial = false;
    public static bool SyringeFilledUp = false;
    public static bool MedicationAdmistered = false;

    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject LeftGlove;
    public GameObject RightGlove;
    public GameObject Syringe;
    public GameObject Vial;
    public GameObject PatientArm;
    public void PutOnLeftGlove()
    {
        LeftGloveOn = true;
        LeftGlove.SetActive(true);
    }
    public void PutOnRightGlove()
    {
        RightGloveOn = true;
        RightGlove.SetActive(true);
    }
    public void PickUpSyringe()
    {
        if(LeftGloveOn && RightGloveOn)
        {

        }
        else
        {
            ResultsController.Current.WriteText("Syringe Picked Up Without Gloves On");
        }
    }
    public void PlaceSyringeInVial()
    {
        SyringeInVial = true;
        SyringeFilledUp = true;

    }
    public void PlaceSyringeInPatient()
    {
        if (SyringeFilledUp)
        {
            ResultsController.Current.WriteText("Pass!");
        }
        else
        {
            ResultsController.Current.WriteText("No Medication In Syringe");
        }
    }
}
