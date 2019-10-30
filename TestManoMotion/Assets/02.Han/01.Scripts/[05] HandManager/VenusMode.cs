using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusMode : Mode
{
    InteractableDrone drone;
    public VenusMode(PrimeHand hand) : base(hand)
    {
        this.hand = hand;
        drone = GameObject.FindObjectOfType<InteractableDrone>();
    }

    //public override void OnTriggeredGrab() => hand.curObj.ProcessGrab();
    public override void OnTriggeredPick()
    {
        if (hand.curObj != null)
            hand.curObj.ProcessPick();
    }
    public override void OnTriggeredDrop()
    {
        if (hand.curObj != null)
            hand.curObj.ProcessDrop();
    }

    public override void OnTriggeredRelease() => drone.ReturnBack();
}
