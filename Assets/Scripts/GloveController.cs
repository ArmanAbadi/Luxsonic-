using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GloveController : ProximityTrigger
{
    public void Start()
    {
        Targets = new GameObject[2];
        Targets[0] = GameController.Current.LeftHand;
        Targets[1] = GameController.Current.RightHand;
    }
    public override void Action(GameObject Target)
    {
        WearGlove(Target);
    }
    public void WearGlove(GameObject Target)
    {
        if (Target == GameController.Current.LeftHand)
        {
            transform.parent = Target.transform;
            transform.position = Target.transform.position;

            GameController.Current.PutOnLeftGlove();

            Destroy(gameObject);
        }
        if (Target == GameController.Current.RightHand)
        {
            transform.parent = Target.transform;
            transform.position = Target.transform.position;

            GameController.Current.PutOnRightGlove();

            Destroy(gameObject);
        }
    }
}
