using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	public GameObject UIInfo_img;
	public Text content_txt;

	private void Awake()
	{
		if (instance == null) instance = GetComponent<UIManager>();
		else Destroy(this);
	}

	public IEnumerator ShowInfoUI(string content)
	{

		content_txt.text = content;

		//왼쪽 바 나오고 들어감
		iTween.MoveTo(UIInfo_img, iTween.Hash("x", 25, "time", 3.0f, "easetype", iTween.EaseType.easeOutQuad));
		yield return new WaitForSeconds(5.0f);
		iTween.MoveTo(UIInfo_img, iTween.Hash("x", -500, "time", 3.0f, "easetype", iTween.EaseType.easeOutQuad));
	}

}
