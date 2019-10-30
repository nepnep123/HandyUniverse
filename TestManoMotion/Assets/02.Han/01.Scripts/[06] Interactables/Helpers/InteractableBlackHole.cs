using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBlackHole : InteractableTrinity
{
    public override void ProcessPick()
    {
        //Todo: 행성 밖으로...
        GameManager.instance.masterBook.ClosePortal();
        helper.OpenObjects(false);
    }
}
