using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    Animator anim;

    public float InitPage()
    {
        anim = GetComponent<Animator>();
        float pageAnimTime = 0;
        RuntimeAnimatorController acp = anim.runtimeAnimatorController;    //Get Animator controller
        for (int i = 0; i < acp.animationClips.Length; i++)                 //For all animations
        {
            if (acp.animationClips[i].name == "PageAnim")        //If it has the same name as your clip
            {
                pageAnimTime = acp.animationClips[i].length;
            }
        }
        return pageAnimTime;
    }
    public void OnOffPage(bool booleana)
    {
        gameObject.SetActive(booleana);
    }
    public void PNPage(bool booleana)
    {
		SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.nextpageSound);
		if (booleana == true)
        {
            anim.Play("PageAnim");
        }
        else
        {
            anim.Play("PageAnim2");
        }
    }
}
