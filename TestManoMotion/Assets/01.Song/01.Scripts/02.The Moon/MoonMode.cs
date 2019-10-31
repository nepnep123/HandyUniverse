using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMode : Mode
{

	public MoonMode(PrimeHand hand) : base(hand)
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
			hand.curObj.ProcessGrab();
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
