using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KTutorialMode : Mode
{
    public static bool grablockdIs = true;

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
        Debug.Log(123);
        if (grablockdIs == true)
        {
            KManager.instance.solarGenerate();
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
        if (hand.curObj != null)
            hand.curObj.ProcessRelease();
    }
    // 5 집기놓기
    public override void OnTriggeredClick()
    {
        if (hand.curObj != null)
            hand.curObj.ProcessClick();
    }
}
