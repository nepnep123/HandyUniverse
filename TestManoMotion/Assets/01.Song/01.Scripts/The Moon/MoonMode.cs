using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMode : Mode
{
	public bool isFinished;

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
		//행성안에 UI가 모두 끝나야지 Grab제스쳐로 나갈수 있다. 
		if (isFinished == true && UIManager.instance.canMove == true)
		{
			MoonWorld.instance.AbleExitPotal();
			isFinished = false;
		}

		else {
			if(hand.curObj != null)
				hand.curObj.ProcessGrab();
		}
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
