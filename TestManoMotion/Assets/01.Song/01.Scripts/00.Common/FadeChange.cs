using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeChange : MonoBehaviour
{
	private Animator anim;

	private void Start()
	{
		//자식중에 FadeOut이라는 객체를 찾아 온다. (다른 효과를 위해 Find 사용)
		anim = transform.Find("FadeOut").GetComponent<Animator>();
	}

	public void FadeOut()
	{
		anim.SetBool("FadeChange", true);
	}
	public void FadeIn()
	{
		anim.SetBool("FadeChange", false);
	}
}
