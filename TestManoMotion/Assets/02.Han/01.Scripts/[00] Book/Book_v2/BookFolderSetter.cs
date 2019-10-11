using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookFolderSetter : BookPageSetter
{
    /*FolderInfo folderInfo;
    public enum BlackWhite { left = 0, right = 2 }
    public override void InitBookSetter()
    {
        base.InitBookSetter();
        for (int i = 0; i < 4; i++)
        {
            if (i > folderInfo.lastPhotoIndex)
                SetIndividualPhoto(i, null);
            else
                SetIndividualPhoto(i, folderInfo.photos[i]);
        }
    }

    public void SetFolder(FolderInfo folderInfo)
    {
        this.folderInfo = folderInfo;
    }

    protected override void PageStartSub(bool booleana) => SetPagePhoto(booleana);

    protected override void PageEndSub(bool booleana) => SetEndBookPhoto(booleana);

    void SetPagePhoto(bool booleana)
    {
        if (booleana == true)
        {
            if (folderInfo.curBookPlaneIndex == folderInfo.maxBookPlaneIndex) return;
            SetPagePhoto(BlackWhite.left, BlackWhite.right);//넘길때 페이지포토 왼쪽은 현재의 오른쪽
            folderInfo.curBookPlaneIndex++;
            SetBookPhoto(BlackWhite.right, BlackWhite.right);//넘길때 책포토는 다음의 오른쪽
            SetPagePhoto(BlackWhite.right, BlackWhite.left);//넘길때 페이지포토는 다음의 왼쪽
        }
        else
        {
            if (folderInfo.curBookPlaneIndex == 0) return;
            SetPagePhoto(BlackWhite.right, BlackWhite.left);//넘길때 페이지포토 왼쪽은 현재의 오른쪽
            folderInfo.curBookPlaneIndex--;
            SetBookPhoto(BlackWhite.left, BlackWhite.left);//넘길때 책포토는 다음의 오른쪽
            SetPagePhoto(BlackWhite.left, BlackWhite.right);//넘길때 페이지포토는 다음의 왼쪽
        }
    }

    void SetEndBookPhoto(bool booleana)
    {
        if (booleana == true)
            SetBookPhoto(BlackWhite.left, BlackWhite.left);//넘길때 책포토는 다음의 오른쪽
        else
            SetBookPhoto(BlackWhite.right, BlackWhite.right);//넘길때 책포토는 다음의 오른쪽
    }

    void SetIndividualPhoto(int index, Texture2D texture)
    {
        switch (index)
        {
            case 0:
                if (texture == null)
                    book.bookPages[0].photo[0].color = new Color(0, 0, 0, 0);
                else
                {
                    book.bookPages[0].photo[0].color = new Color(1, 1, 1, 1);
                    book.bookPages[0].photo[0].texture = texture;
                }
                break;
            case 1:
                if (texture == null)
                    book.bookPages[0].photo[1].color = new Color(0, 0, 0, 0);
                else
                {
                    book.bookPages[0].photo[1].color = new Color(1, 1, 1, 1);
                    book.bookPages[0].photo[1].texture = texture;
                }
                break;
            case 2:
                if (texture == null)
                    book.bookPages[1].photo[0].color = new Color(0, 0, 0, 0);
                else
                {
                    book.bookPages[1].photo[0].color = new Color(1, 1, 1, 1);
                    book.bookPages[1].photo[0].texture = texture;
                }
                break;
            case 3:
                if (texture == null)
                    book.bookPages[1].photo[1].color = new Color(0, 0, 0, 0);
                else
                {
                    book.bookPages[1].photo[1].color = new Color(1, 1, 1, 1);
                    book.bookPages[1].photo[1].texture = texture;
                }
                break;
        }
    }
    void SetIndividualPagePhoto(int index, Texture2D texture)
    {
        switch (index)
        {
            case 0:
                if (texture == null)
                    book.centerPages[0].photo[0].color = new Color(0, 0, 0, 0);
                else
                {
                    book.centerPages[0].photo[0].color = new Color(1, 1, 1, 1);
                    book.centerPages[0].photo[0].texture = texture;
                }
                break;
            case 1:
                if (texture == null)
                    book.centerPages[0].photo[1].color = new Color(0, 0, 0, 0);
                else
                {
                    book.centerPages[0].photo[1].color = new Color(1, 1, 1, 1);
                    book.centerPages[0].photo[1].texture = texture;
                }
                break;
            case 2:
                if (texture == null)
                    book.centerPages[1].photo[0].color = new Color(0, 0, 0, 0);
                else
                {
                    book.centerPages[1].photo[0].color = new Color(1, 1, 1, 1);
                    book.centerPages[1].photo[0].texture = texture;
                }
                break;
            case 3:
                if (texture == null)
                    book.centerPages[1].photo[1].color = new Color(0, 0, 0, 0);
                else
                {
                    book.centerPages[1].photo[1].color = new Color(1, 1, 1, 1);
                    book.centerPages[1].photo[1].texture = texture;
                }
                break;
        }
    }
    void SetPagePhoto(BlackWhite pageLR, BlackWhite photoLR)
    {
        SetIndividualPagePhoto((int)pageLR, folderInfo.photos[(folderInfo.curBookPlaneIndex * 4) + (int)photoLR]);
        SetIndividualPagePhoto((int)pageLR + 1, folderInfo.photos[(folderInfo.curBookPlaneIndex * 4) + (int)photoLR + 1]);
    }
    void SetBookPhoto(BlackWhite pageLR, BlackWhite photoLR)
    {
        SetIndividualPhoto((int)pageLR, folderInfo.photos[(folderInfo.curBookPlaneIndex * 4) + (int)photoLR]);
        SetIndividualPhoto((int)pageLR + 1, folderInfo.photos[(folderInfo.curBookPlaneIndex * 4) + (int)photoLR + 1]);
    }*/
}
