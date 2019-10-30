using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusMode : Mode
{
    public Helper helper;
    public VenusMode(PrimeHand hand) : base(hand)
    {
        this.hand = hand;
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

    public override void OnTriggeredRelease() => helper.drone.ReturnBack();
}
