using UnityEngine;

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

    public virtual void OnHandCollideEnter(ICollidable col) { }      //충돌
    public virtual void OnHandCollideExit(ICollidable col) { }      //충돌
    public virtual void OnTriggeredGrab() { }       //그랩
    public virtual void OnTriggeredRelease() { }    //릴리즈
    public virtual void OnTriggeredPick() { }       //픽
    public virtual void OnTriggeredDrop() { }       //드랍
    public virtual void OnTriggeredClick() { }      //클릭
}

public class TestMode : Mode
{
    int a = 0;
    int b = 0;
    int c = 0;
    int d = 0;
    public TestMode(PrimeHand hand) : base(hand)
    {
        this.hand = hand;
    }

    public override void OnTriggeredRelease()
    {
        a++;
        TestManager.instance.testinas.text = "Release" + a;
    }

    public override void OnTriggeredGrab()
    {
        b++;
        TestManager.instance.testlin.text = "Grab" + b;
    }
    public override void OnTriggeredPick()
    {
        c++;
        TestManager.instance.testan.text = "Pick" + c;
    }
    public override void OnTriggeredDrop()
    {
        d++;
        TestManager.instance.testels.text = "Drop" + d;
    }
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

    public override void OnHandCollideEnter(ICollidable col)
    {
        col.ProcessCollision();
    }

    public override void OnTriggeredRelease()
    {
        Debug.Log("포탈 생성!!");
        hand.curObj.GetComponent<ICollidable>().ProcessMethod();
        Debug.Log("포탈생성 완료!");
    }

    public override void OnTriggeredGrab()
    {
        Debug.Log("EntryMode : Grab!@!");
    }
}
