using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableInfoma : InteractableTrinity
{
    [SerializeField] VenusIdentifier[] venuses;
    public override void ProcessInit<T>(T obj)
    {
        base.ProcessInit(obj);
        var a = GetComponentsInChildren<VenusIdentifier>(true);
        venuses = new VenusIdentifier[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < a.Length; j++)
            {
                if (i == (int)a[j].venusPos) venuses[i] = a[j];
            }
        }
        CloseInfos();
    }

    public override void ProcessPick()
    {
        if (helper.drone.releaseStack.Count > 1)
            helper.drone.ReturnBack();
        isPicked = true;
        venuses[(int)helper.curPos].gameObject.SetActive(true);
        helper.drone.releaseStack.Push(CloseInfos);
    }

    public void CloseInfos()
    {
        isPicked = false;
        for(int i = 0; i < venuses.Length; i++)
        {
            venuses[i].gameObject.SetActive(false);
        }
    }
}
