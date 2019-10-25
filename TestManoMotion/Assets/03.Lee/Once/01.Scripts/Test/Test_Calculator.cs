using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Calculator : MonoBehaviour
{
    // 좋은 예시
    Transform othersTr1;

    public int hp = 100;

    void Add(ref int hp)
    {
        hp = hp * 2;
    }

    void Min(Vector3 pos)
    {
        pos += Vector3.forward;
    }

    void Abc(Transform tr)
    {
        tr.position += Vector3.forward;
    }
    
    private void Start()
    {
        //Add(hp);
        //Min(transform.position);
        //Abc(transform);

        // 안좋은 예시 => 가비지컬렉터
        Transform othersTr2;
        othersTr1 = FindObjectOfType<Transform>();

        // 좋은 예시 => 메모리는 잡더라도 확실하게 상주하고 있어 가비지컬렉터가 신경 안씀
        othersTr2 = FindObjectOfType<Transform>();
    }

    
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    MissionClear();
        //}

        // 값형식 -> stack, 예외가 있어
        // 복사하여 진짜가 아님
        if (Input.GetKeyDown(KeyCode.A))
        {
            Min(transform.position);
        }

        // 참조형식(주소로 가서 '직접' 참조하겠다)과 차이 -> heap
        // transform의 주소로 보내
        if (Input.GetKeyDown(KeyCode.B))
        {
            Add(ref hp);
            Abc(transform);
        }

        // 그렇다면 어떨때 써야하는가?
        // 1. 최적화 -> 값 형식

        // stack : 할당 및 해제 편해, 빨라
        // heap : 

        // 유니티는 참조형의 존재하는 애들을 전부 heap에 저장한다

    }








    // delegate 정의 순서 1번 째 : 
    public delegate void VoidMyDelegate();

    // delegate 정의 순서 2번 째 : 선언
    public VoidMyDelegate OnMissionClear;

    private void Awake()
    {
        // delegate 정의 순서 3번 째 : 선언OnMissionClear라는 대리자에 Friend1Happy, Friend2Happy 함수를 '구독'시킨다.
        // ※ 향 후에 구독해제도 필수!
        OnMissionClear += Friend1Happy;
        OnMissionClear += Friend2Happy;
        //MissionClear();
    }

    // GameObject가 비활성화되거나 삭제될 때 실행
    private void OnDisable()
    {
        OnMissionClear -= Friend1Happy;
        OnMissionClear -= Friend2Happy;
    }


    public void MissionClear()
    {
    // delegate 정의 순서 4번 째 : OnMissionClear 대리자를 호출한다. = 이벤트를 발생시킨다.
        OnMissionClear();
    }

    public void Friend1Happy()
    {
        Debug.Log("친구1이 행복해합니다");
    }

    public void Friend2Happy()
    {
        print("친구2가 행복해합니다.");
    }
}