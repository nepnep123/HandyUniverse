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
        {
            if (helper.isPickable == false) return;
            hand.curObj.ProcessPick();
            SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.venusSoundPack.venusIconPick);
        }
    }
    public override void OnTriggeredDrop()
    {
        if (hand.curObj != null)
        {
            if (helper.isPickable == false) return;
            hand.curObj.ProcessDrop();
        }
    }

    //public override void OnTriggeredRelease() => helper.drone.ReturnBack();
    public override void OnTriggeredGrab()
    {
        if (helper.isPickable == false) return;
        GameManager.instance.hand.curObj = null;
        SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.venusSoundPack.venusRelease);
        helper.drone.ReturnBack();
    }
}
