using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITweenManager : MonoBehaviour
{
	public static Hashtable ht1;

    void Start()
    {

		ht1 = new Hashtable();
		ht1.Add("path", iTweenPath.GetPath("eagle"));
		ht1.Add("time", 15);
		ht1.Add("easetype", iTween.EaseType.linear);
		//ht1.Add("looptype", iTween.LoopType.loop);
		ht1.Add("movetopath", false);
		ht1.Add("orienttopath", true);
		ht1.Add("oncompletetarget", this.gameObject);

		//5초뒤에 다시 날기 시작
		ht1.Add("oncomplete", "Com");

		
	}

	public void Com()
	{
		StartCoroutine(Complete());
	}

	IEnumerator Complete()
	{
		yield return new WaitForSeconds(5.0f);

		iTween.MoveTo(EnterPotal.eagleprefab, ht1);

	}
}
