using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGallary : InteractableTrinity
{
    public InteractablePhoto photoPrefab;
    public List<InteractablePhoto>[] photos;
    bool isOpened = false;

    public InteractablePhoto curSelectedPhoto;

    public override void ProcessInit<T>(T obj)
    {
        photos = new List<InteractablePhoto>[3];
        base.ProcessInit(obj);
        //찾아낸 텍스쳐투디들 : 어쩔 수 없는 하드코드
        var lakshmis = PhotoUtils.ReadTexturesInFolder("Venus_Lakshmi");
        var maxwells = PhotoUtils.ReadTexturesInFolder("Venus_Maxwell");
        var venusis = PhotoUtils.ReadTexturesInFolder("Venus_Venus");
        List<InteractablePhoto> photongs = new List<InteractablePhoto>();
        List<InteractablePhoto> photonis = new List<InteractablePhoto>();
        List<InteractablePhoto> photoves = new List<InteractablePhoto>();
        for(int i = 0; i< lakshmis.Count; i++)
        {
            var a = Instantiate(photoPrefab, transform);
            a.transform.localPosition = Vector3.zero;
            a.GetComponent<MeshRenderer>().material.mainTexture = lakshmis[i];
            photongs.Add(a);
        }
        photongs.Add(Instantiate(photoPrefab, transform));
        for(int i =0; i< maxwells.Count; i++)
        {
            var a = Instantiate(photoPrefab, transform);
            a.transform.localPosition = Vector3.zero;
            a.GetComponent<MeshRenderer>().material.mainTexture = maxwells[i];
            photonis.Add(a);
        }
        for(int i = 0; i < venusis.Count; i++)
        {
            var a = Instantiate(photoPrefab, transform);
            a.transform.localPosition = Vector3.zero;
            a.GetComponent<MeshRenderer>().material.mainTexture = venusis[i];
            photoves.Add(a);
        }
        photos[0] = photongs;
        photos[1] = photonis;
        photos[2] = photoves;
        //모든 사진들 초기화
        for (int i = 0; i < photos.Length; i++)
        {
            foreach (InteractablePhoto ip in photos[i])
            {
                ip.ProcessInit(this);
            }
        }
        OpenCloseAll(false);
    }
    public void OpenCloseAll(bool booleana)
    {
        for(int i = 0; i < photos.Length; i++)
        {
            foreach(InteractablePhoto photo in photos[i])
            {
                photo.gameObject.SetActive(booleana);
            }
        }
    }
    public override void ProcessPick()
    {
        //helper.drone.releaseStack.Push(CloseGallery);
        //Todo : 갤러리를 열어라
        OpenCloseAll(false);
        OpenGallery(helper.curPos);
        helper.drone.releaseStack.Push(CloseGallery);
    }

    void OpenGallery(VenusPos vs)
    {
        ActivePositionPhoto(vs);
        for (int i = 0; i < photos[(int)vs].Count; i++)
        {
            Vector3 front = (helper.target.transform.position + (helper.target.transform.forward));    //센터에서 타겟을 바라보는 벡터
            int halfI = photos[(int)vs].Count / 2;
            Quaternion rot = Quaternion.AngleAxis((-halfI + i) * 35f, Vector3.up);
            Debug.Log(i * 20f);
            Vector3 rottedVector = rot * front;
            rottedVector = rottedVector.normalized * 0.6f;
            Vector3 finalDestination = helper.target.transform.position + rottedVector;
            //Vector3 rottedVector = front;
            //Debug.Log(rottedVector);
            photos[(int)vs][i].StartSpread(finalDestination);
        }
        /*
        foreach (InteractablePhoto ip in photos[(int)vs])
        {
            ip.StartSpread();
        }*/
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 0.4f);
    }

    void CloseGallery()
    {
        for(int i = 0; i < photos.Length; i++)
        {
            foreach (InteractablePhoto ip in photos[i])
            {
                if(ip.gameObject.activeSelf)
                    ip.StartShrink();
            }
        }
    }
    void ActivePositionPhoto(VenusPos vs)
    {
        foreach(InteractablePhoto ip in photos[(int)vs])
        {
            ip.gameObject.SetActive(true);
        }
    }
    public void InactiveAllOutline()
    {
        foreach (InteractablePhoto ip in photos[(int)helper.curPos])
        {
            ip.outliner.SetActive(false);
        }
    }
}
