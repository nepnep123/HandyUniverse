using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePhoto : InteractableObject
{
    public Transform target;

    InteractableGallary gallary;
    Helper helper;
    bool isInited = false;
    float originalScale = 0;
    float scaledScale = 0;
    public bool isOpended = false;
    public GameObject outliner;

    public override void ProcessInit<T>(T obj)
    {
        if (obj is InteractableGallary)
        {
            gallary = (obj as InteractableGallary);
            target = gallary.helper.target;
            helper = gallary.helper;
        }
        isInited = true;
        outliner = transform.Find("Outliner").gameObject;
        outliner.SetActive(false);
        originalScale = transform.localScale.x;
        scaledScale = originalScale * 3f;
    }

    public override void ProcessCollisionEnter()
    {
        Debug.Log("충돌시작");
        gallary.InactiveAllOutline();
        outliner.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Scalar());
    }
    public override void ProcessCollisionExit()
    {
        Debug.Log("충돌 ㅌㅌ");
        gallary.InactiveAllOutline();
        StopAllCoroutines();
        StartCoroutine(Unscalar());
    }
   
    IEnumerator Scalar()
    {
        isOpended = true;
        float scalar = scaledScale;
        float loScale = transform.localScale.x;
        while (loScale < scalar)
        {
            float lerped = Mathf.Lerp(loScale, scalar, 2f * Time.deltaTime);
            loScale = lerped;
            transform.localScale = new Vector3(loScale, loScale, loScale);
            yield return null;
        }
    }

    IEnumerator Unscalar()
    {
        isOpended = false;
        float scalar = originalScale;
        float loScale = transform.localScale.x;
        while (loScale > scalar)
        {
            float lerped = Mathf.Lerp(loScale, scalar, 2f * Time.deltaTime);
            loScale = lerped;
            transform.localScale = new Vector3(loScale, loScale, loScale);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //바라보기
        if (isInited == false) return;
        Rotating();
        //이동하기
        if (isOpended == false) return;
        Moving();
    }

    public void StartShrink()
    {
        //StopAllCoroutines();
        transform.SetParent(null);
        StartCoroutine(ShrinkThis());
    }

    IEnumerator ShrinkThis()
    {
        float dist = Vector3.Distance(transform.position, gallary.transform.position);
        while (dist > 0.1f)
        {
            dist = Vector3.Distance(transform.position, gallary.transform.position);
            Vector3 lerped = Vector3.Lerp(transform.position, gallary.transform.position, 2f * Time.deltaTime);
            transform.localPosition = lerped;
            yield return null;
        }
        transform.SetParent(gallary.transform);
        gameObject.SetActive(false);
    }

    public void StartSpread(Vector3 desti)
    {
        //StartCoroutine(SpreadThis());
        StartCoroutine(SpreadHello(desti));
    }
    IEnumerator SpreadHello(Vector3 desti)
    {
        transform.SetParent(null);
        //transform.localPosition = Vector3.zero;
        float dist = Vector3.Distance(transform.position, desti);
        while (dist > 0.01f)
        {
            Vector3 lerped = Vector3.Lerp(transform.position, desti, 2f * Time.deltaTime);
            transform.position = new Vector3(lerped.x, target.position.y, lerped.z);
            dist = Vector3.Distance(transform.position, desti);
            yield return null;
        }
    }
    private void Moving()
    {
        //헬퍼가 타겟을 바라보는 포지션
        if (Vector3.Distance(transform.position, helper.target.position) < 0.8f) return;
        Vector3 dir = helper.target.transform.position - helper.transform.position;
        //목적지 : 지금 위치에서 방향을 더한 값
        Vector3 desti = transform.position + dir.normalized * Time.deltaTime * 80f;
        Vector3 lerped = Vector3.Lerp(transform.position, desti, 0.6f * Time.deltaTime);
        transform.position = lerped;
    }

    private void Rotating()
    {
        transform.LookAt(target);
        transform.Rotate(transform.up, 180f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
    }
}
