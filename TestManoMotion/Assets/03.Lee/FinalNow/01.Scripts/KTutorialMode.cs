using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KTutorialMode : Mode
{
    public KWorld kworld;

    public KTutorialMode(PrimeHand hand) : base(hand)
    {
        this.hand = hand;
    }

    public override void ModeChange(Mode mode)
    {
        base.ModeChange(mode);
    }

    // 1 주먹
    public override void OnTriggeredGrab()
    {
        if(kworld.isFirstMissionStarted == true)
        {
            kworld.CountFirstMission(ManoGestureTrigger.GRAB);
        }

        if (kworld.isGoodBye == true)
        {
            kworld.CountGoodBye(ManoGestureTrigger.GRAB);
        }
    }
    // 2 집기
    public override void OnTriggeredPick()
    {
        if (hand.curObj != null)
            hand.curObj.ProcessPick();
    }
    // 3 놓기
    public override void OnTriggeredDrop()
    {
        if (hand.curObj != null)
            hand.curObj.ProcessDrop();
    }
    // 4 보자기
    public override void OnTriggeredRelease()
    {
        if (kworld.isFirstMissionStarted == true)
        {
            kworld.CountFirstMission(ManoGestureTrigger.RELEASE);
        }
    }
    // 5 집기놓기
    public override void OnTriggeredClick()
    {
        if (kworld.isSecondMissionStarted == true)
        {
            kworld.CountSecondMission(ManoGestureTrigger.CLICK);
        }
    }
}
