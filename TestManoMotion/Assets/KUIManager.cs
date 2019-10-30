using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KUIManager : MonoBehaviour
{
    private Button buttonWorsding;

    private Animator animator;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(3f);

    private void Start()
    {
        animator = GetComponent<Animator>();
        buttonWorsding = GetComponent<Button>();

        buttonWorsding.enabled = false;
        buttonWorsding.transform.GetChild(0).GetComponent<Text>().text = "MM에 오신걸 환영합니다";
        StartCoroutine(PrimalWording());
    }

    IEnumerator PrimalWording()
    {
        yield return waitForSeconds;
        animator.SetInteger("LOCK", 1);
    }

    public void Done()
    {
        buttonWorsding.enabled = true;
    }
}
