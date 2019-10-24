using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUtils
{
    static public void OnOffCanvasGroup(CanvasGroup cg, bool booleana)
    {
        cg.blocksRaycasts = booleana;
        cg.interactable = booleana;
        cg.alpha = booleana ? 1f : 0;
    }
}
