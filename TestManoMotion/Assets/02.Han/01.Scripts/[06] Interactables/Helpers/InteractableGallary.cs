using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGallary : InteractableTrinity
{
    
    public InteractablePhoto photoPrefab;
    public List<InteractablePhoto>[] photos;
    bool isOpened = false;
    
    public override void ProcessInit<T>(T obj)
    {
        photos = new List<InteractablePhoto>[2];
        base.ProcessInit(obj);
        //찾아낸 텍스쳐투디들 : 어쩔 수 없는 하드코드
        var lakshmis = PhotoUtils.ReadTexturesInFolder("Venus_Lakshmi");
        var maxwells = PhotoUtils.ReadTexturesInFolder("Venus_Maxwell");
        List<InteractablePhoto> photongs = new List<InteractablePhoto>();
        List<InteractablePhoto> photonis = new List<InteractablePhoto>();
        for(int i = 0; i< lakshmis.Count; i++)
        {
            var a = Instantiate(photoPrefab, transform);
            a.GetComponent<MeshRenderer>().material.mainTexture = lakshmis[i];
            photongs.Add(a);
        }
        photongs.Add(Instantiate(photoPrefab, transform));
        for(int i =0; i< maxwells.Count; i++)
        {
            var a = Instantiate(photoPrefab, transform);
            a.GetComponent<MeshRenderer>().material.mainTexture = maxwells[i];
            photonis.Add(a);
        }
        photos[0] = photongs;
        photos[1] = photonis;
        OpenCloseAll(false);
    }
    public void OpenCloseAll(bool booleana)
    {
        for(int i = 0; i < 2; i++)
        {
            foreach(InteractablePhoto photo in photos[i])
            {
                photo.gameObject.SetActive(booleana);
            }
        }
    }

    /*
    public override void ProcessPick()
    {
        helper.drone.releaseStack.Push(CloseGallery);
        //Todo : 갤러리를 열어라
        OpenCloseAll(true);
        Debug.Log("fuck1");
        switch (helper.curPos)
        {
            case VenusPos.Lakshmi:
                Debug.Log("fuck1_1");
                OpenGallery(0);
                break;
            case VenusPos.Maxwell:
                OpenGallery(1);
                break;
            case VenusPos.Venus:
                break;
        }
    }

    void Update()
    {
        if(isOpened == false)
            transform.Rotate(Vector3.up, 45f * Time.deltaTime, Space.World);
        else
            transform.LookAt(helper.target);
    }

    public void OpenGallery(int index)
    {
        Debug.Log("fuck2");
        isOpened = true;
        StartCoroutine(SpreadObjs(true, index));
    }

    public void CloseGallery()
    {
        Debug.Log("fuck");
        isOpened = false;
        switch (helper.curPos)
        {
            case VenusPos.Lakshmi:
                StartCoroutine(SpreadObjs(false, 0));
                break;
            case VenusPos.Maxwell:
                StartCoroutine(SpreadObjs(false, 1));
                break;
            case VenusPos.Venus:
                break;
        }
    }

    IEnumerator SpreadObjs(bool booleana, int index)
    {
        float timer = 0;
        while (timer < 0.75f)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < photos[index].Count; i++)
            {
                Vector3 localPos = photos[index][i].transform.localPosition;
                Vector3 randomVect = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), 0);
                Vector3 targetPos = booleana ? localPos + randomVect : Vector3.zero;
                Vector3 lerped = Vector3.Lerp(localPos, targetPos, Time.deltaTime * 0.8f);
                //timer * 0.015f
                photos[index][i].transform.localPosition = lerped;
            }
            yield return null;
        }
        if (booleana == false)
        {
            for (int i = 0; i < photos[index].Count; i++)
            {
                photos[index][i].transform.localPosition = Vector3.zero;
            }
        }
    }
    */
}
