using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    //ComputeShader, GPU, Flocking 알고리즘
    public Transform target;
    public InteractableTrinity[] interacts;

    public InteractableDrone drone;
    public int infoIndex = 0;
    public VenusPos curPos = VenusPos.Venus;

    public Transform arDevice;
    public bool isAbleToLook = true;

    public Helper_Raycaster raycaster;
    // Start is called before the first frame update
    void Awake()
    {
        drone = GetComponent<InteractableDrone>();
        drone.ProcessInit(this);
        for(int i = 0; i < interacts.Length; i++)
        {
            //interacts[i].ProcessInit(this);
            if (interacts[i] is InteractableInfoma)
                infoIndex = i;
        }
        Activer(false);
        //인게임
        //target = GameManager.instance.camPos;
    }
    private void OnEnable()
    {
        if (GameManager.instance != null)
        {
            //target = GameManager.instance.camPos;
            target = Camera.main.transform;
            arDevice = GameManager.instance.camPos;
            raycaster = GetComponent<Helper_Raycaster>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //따라가기
        Vector3 pPos = new Vector3(target.position.x, 0.5f, target.position.z);
        Vector3 thisPos = transform.position;
        float dist = Vector3.Distance(pPos, thisPos);
        if (dist > 1f)
        {
            Vector3 lerpMove = Vector3.Lerp(thisPos, pPos, Time.deltaTime * 0.6f);
            transform.position = lerpMove;
        }
        //by재현, 두둥실, 1015
        float ocil = Mathf.Sin(1.57f * Time.time);
        transform.position += new Vector3(0, ocil * 0.002f, 0);
        //바라보기
        if (isAbleToLook == true)
        {
            if (dist > 0.2f)
            {
                transform.LookAt(target);
                Quaternion rot = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                transform.rotation = rot;
            }
        }
    }
    public void OpenObjects(bool booleana)
    {
        StopAllCoroutines();
        //여기까지 다들 접어오다가 끝나면 이녀석도 돌아와야한다.
        StartCoroutine(SpreadObjs(booleana));
    }
    IEnumerator SpreadObjs(bool booleana)
    {
        if(booleana)
        {
            Activer(true);
            isAbleToLook = false;
        }
        float timer = 0;
        while (timer < 0.75f)
        {
            timer += Time.deltaTime;
            //자식 인터렉츠의 숫자만큼 퍼지기 실행
            for(int i = 0; i < interacts.Length; i++)
            {
                Vector3 localPos = interacts[i].transform.localPosition;
                Vector3 firstPos = new Vector3(1f, 0.2f, 0.4f);
                Quaternion anxis = Quaternion.AngleAxis(45f * i+1, Vector3.forward);
                Vector3 targetPos = booleana ? anxis * firstPos : Vector3.zero;
                targetPos.y = 0.2f;
                Vector3 lerped = Vector3.Lerp(localPos, targetPos, Time.deltaTime * 0.8f);
                interacts[i].transform.localPosition = lerped;
            }
                yield return null;
        }
        if(booleana == false)
        {
            for(int i = 0; i < interacts.Length; i++)
            {
                interacts[i].transform.localPosition = Vector3.zero;
            }
            Activer(false);
            isAbleToLook = true;
        }
    }

    public void AllDeselect()
    {
        for(int i = 0; i < interacts.Length; i ++)
        {
            interacts[i].SelectObj(false);
        }
    }
    void Activer(bool booleana)
    {
        foreach (InteractableTrinity inter in interacts)
        {
            inter.gameObject.SetActive(booleana);
        }
    }
}
