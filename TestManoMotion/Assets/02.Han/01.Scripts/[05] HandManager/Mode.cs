﻿using UnityEngine;

public class Mode
{
    public PrimeHand hand;

    public Mode(PrimeHand hand)
    {
        this.hand = hand;
    }

    public virtual void ModeChange(Mode mode)
    {
        hand.mode = mode;
    }

    public virtual void OnTriggeredGrab() { }       //그랩
    public virtual void OnTriggeredRelease() { }    //릴리즈
    public virtual void OnTriggeredPick() { }       //픽
    public virtual void OnTriggeredDrop() { }       //드랍
    public virtual void OnTriggeredClick() { }      //클릭
}

public class EntryMode : Mode
{
	
    public EntryMode(PrimeHand hand):base(hand)
    {
        this.hand = hand;
    }

    public override void ModeChange(Mode mode)
    {
        base.ModeChange(mode);
    }

    public override void OnTriggeredGrab()
    {
		if (hand.curObj != null)
			hand.curObj.ProcessPick();
	}
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
	public override void OnTriggeredRelease()
	{
		if (hand.curObj != null)
			hand.curObj.ProcessRelease();
	}
	public override void OnTriggeredClick()
	{
        if (hand.curObj != null)
            hand.curObj.ProcessClick();
	}
}

// 예시 2
//public class MarsMode : Mode
//{
//    public MarsMode(PrimeHand hand):base(hand)
//    {
//        this.hand = hand;
//    }

//    public override void OnTriggeredGrab()
//    {
//        GameObject.Destroy(hand.curObj);
//    }
//}