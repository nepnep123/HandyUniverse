using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;


	public Image background;
	public Text mission_txt;

	[Header("Moon UI")]
	public GameObject moonExit_ui;
	public GameObject handprintInfo;

	private Animator anim;

    //재현's UI
    public VenusInstructor venusInstruct;
    //강원's UI
    public KVenusInstructor KvenusInstruct;
    public KVenusInstructor2 KvenusInstruct2;
    public KVenusInstructor3 KvenusInstruct3;
    public KVenusInstructor4 KvenusInstruct4;

    private void Awake()
	{
		if (instance == null) instance = GetComponent<UIManager>();
		else Destroy(this);
		anim = background.GetComponent<Animator>();

		moonExit_ui.SetActive(false);
		handprintInfo.SetActive(false);
	}

	public IEnumerator ShowMissionUI(string content)
	{
		mission_txt.text = content;
		anim.SetBool("IsShowMission", true);
		yield return new WaitForSeconds(5.0f);
		anim.SetBool("IsShowMission", false);
	}
	
	#region 영훈, 문 UI관련
	public IEnumerator MoonUI()
	{
		handprintInfo.SetActive(true);
		yield return new WaitForSeconds(5.0f);
		handprintInfo.SetActive(false);
	}
	#endregion


	#region 재현, 금성 UI관련
	public IEnumerator InstructSequence()
    {
        float timer = 0;
        for(int i = 0; i < venusInstruct.canvasGroups.Length; i++)
        {
            while (timer < 1)
            {
                timer += Time.deltaTime;
                venusInstruct.canvasGroups[i].alpha = timer;
                yield return null;
            }
            yield return new WaitForSeconds(5f);
            timer = 1;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                venusInstruct.canvasGroups[i].alpha = timer;
                yield return null;
            }
        }
    }

    #endregion

    #region 강원, UI관련
    public IEnumerator InstructSequenceK()
    {
        float timer = 0;
        //SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.bgm);
        for (int i = 0; i < KvenusInstruct.canvasGroups.Length; i++)
        {
            while (timer < 1)
            {
                timer += Time.deltaTime;
                KvenusInstruct.canvasGroups[i].alpha = timer;
                yield return null;
            }
            yield return new WaitForSeconds(3f);
            timer = 1;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                KvenusInstruct.canvasGroups[i].alpha = timer;
                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);
        KWorld.instance.isFirstMissionStarted = true;
    }

    public IEnumerator InstructSequenceK2()
    {
        yield return new WaitForSeconds(4f);

        float timer = 0;

        while (timer < 1)
        {
            timer += Time.deltaTime;
            KvenusInstruct2.canvasGroups[0].alpha = timer;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            KvenusInstruct2.canvasGroups[0].alpha = timer;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        KWorld.instance.isSecondMissionStarted = true;
    }

    public IEnumerator InstructSequenceK3()
    {
        yield return new WaitForSeconds(4f);

        float timer = 0;

        while (timer < 1)
        {
            timer += Time.deltaTime;
            KvenusInstruct3.canvasGroups[0].alpha = timer;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            KvenusInstruct3.canvasGroups[0].alpha = timer;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator InstructSequenceK4()
    {
        yield return new WaitForSeconds(4f);

        SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.end);

        float timer = 0;

        while (timer < 1)
        {
            timer += Time.deltaTime;
            KvenusInstruct4.canvasGroups[0].alpha = timer;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            KvenusInstruct4.canvasGroups[0].alpha = timer;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        KWorld.instance.isGoodBye = true;
    }
    #endregion
}
