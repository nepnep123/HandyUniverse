using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    //ComputeShader, GPU, Flocking 알고리즘
    public Transform target;
    public InteractableObject[] interacts;
    InteractableDrone drone;
    public Vector3[] destis;
    // Start is called before the first frame update
    void Awake()
    {
        drone = GetComponent<InteractableDrone>();
        drone.ProcessInit(this);
        foreach(InteractableObject inter in interacts)
        {
            inter.ProcessInit(this);
        }
        //오브젝트들이 퍼질 각각의 위치를 초기화하는 로직
        Vector3 firstPos = new Vector3(0.5f, 0.3f, 0);
        float yFactor = 1;
        destis = new Vector3[3];
        for (int i = 0; i < destis.Length; i++)
        {
            destis[i] += firstPos;
            float yus = 0.2f * yFactor;
            firstPos += new Vector3(-0.5f, yus, 0);
            yFactor *= -1;
        }
        Activer(false);
    }
    private void OnEnable()
    {
        if(GameManager.instance != null)
            target = GameManager.instance.camPos;
    }

    // Update is called once per frame
    void Update()
    {
        //by재현, 두둥실, 1015
        float ocil = Mathf.Sin(1.57f * Time.time);
        transform.position += new Vector3(0, ocil * 0.005f, 0);
        //따라가기
        Vector3 pPos = target.position;
        Vector3 thisPos = transform.position;
        float dist = Vector3.Distance(pPos, thisPos);
        if (dist > 2f)
        {
            Vector3 lerpMove = Vector3.Lerp(thisPos, pPos, Time.deltaTime * 0.6f);
            transform.position = lerpMove;
        }
        //바라보기
        if (dist > 0.2f)
        {
            transform.LookAt(target);
            Quaternion rot = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            transform.rotation = rot;
        }
    }
    public void OpenObjects(bool booleana)
    {
        StopAllCoroutines();
        StartCoroutine(SpreadObjs(booleana));
    }
    /*
    public void OpenPanals()
    {
        foreach (InteractableObject inter in interacts)
        {
            inter.gameObject.SetActive(true);
        }
        StartCoroutine(SpreadObjs(true));
    }

    public void ClosePanals()
    {
        foreach (InteractableObject inter in interacts)
        {
            inter.transform.localPosition = Vector3.zero;
            inter.gameObject.SetActive(false);
        }
    }*/

    IEnumerator SpreadObjs(bool booleana)
    {
        if(booleana)
            Activer(true);
        float timer = 0;
        while (timer < 0.75f)
        {
            timer += Time.deltaTime;
            for(int i = 0; i < interacts.Length; i++)
            {
                Vector3 localPos = interacts[i].transform.localPosition;
                Vector3 targetPos = booleana ? localPos + destis[i] : Vector3.zero;
                Vector3 lerped = Vector3.Lerp(localPos, targetPos, Time.deltaTime * 0.8f);
                //timer * 0.015f
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
            
        }
    }
    void Activer(bool booleana)
    {
        foreach (InteractableObject inter in interacts)
        {
            inter.gameObject.SetActive(booleana);
        }
    }
}
