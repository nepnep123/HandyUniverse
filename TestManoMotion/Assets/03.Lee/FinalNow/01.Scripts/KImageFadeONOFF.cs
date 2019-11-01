using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KImageFadeONOFF : MonoBehaviour
{
    // 두번째 이미지에만 스크립트 붙이면 됨

    public Image imageImage1;
    public Image imageImage2;

    private void Start()
    {
        StartCoroutine(FadeOff());
    }

    IEnumerator FadeOff()
    {
        yield return new WaitForSeconds(1f);

        imageImage1.enabled = false;
        imageImage2.enabled = true;
        
        StartCoroutine(FadeOn());
    }

    IEnumerator FadeOn()
    {
        yield return new WaitForSeconds(1f);

        imageImage1.enabled = true;
        imageImage2.enabled = false;

        StartCoroutine(FadeOff());
    }
}
