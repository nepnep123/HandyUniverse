using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	public Image background;
	public Text mission_txt;

	private Animator anim;
	public bool canMove = false;

	private void Awake()
	{
		if (instance == null) instance = GetComponent<UIManager>();
		else Destroy(this);

		anim = background.GetComponent<Animator>();

	}

	public IEnumerator ShowMissionUI(string content)
	{
		mission_txt.text = content;

		anim.SetBool("IsShowMission", true);
		yield return new WaitForSeconds(5.0f);
		anim.SetBool("IsShowMission", false);
		//UI를 보고나서 3초 뒤에 제스처로 이동한다.
		yield return new WaitForSeconds(3.0f);
		canMove = true;
	}

}
