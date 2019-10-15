using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_friend1 : MonoBehaviour
{
    private void OnEnable()
    {
        FindObjectOfType<Test_Calculator>().OnMissionClear += Friend3Happy;
    }

    public void Friend3Happy()
    {
        Debug.Log("친구3이 행복해합니다");
    }
}