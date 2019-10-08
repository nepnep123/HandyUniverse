using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임매니저가 Texture2D의 리스트를 가지고 있다.
//아니다. 폴더인포가 자기 이름으로된 텍스쳐2D를 가져야한다.
[System.Serializable]
public class FolderInfo
{
    public string folderName = "";
    public List<Texture2D> photos;
    public int lastPhotoIndex = 0;
    public int curBookPlaneIndex = 0;
    public int maxBookPlaneIndex = 0;

    public FolderInfo(List<Texture2D> textures, string folderName)
    {
        photos = new List<Texture2D>();
        this.folderName = folderName;
        lastPhotoIndex = textures.Count - 1;
        int total_Textures_Num = textures.Count + (4 - textures.Count % 4);
        maxBookPlaneIndex = (total_Textures_Num / 4) - 1;
        for (int i = 0; i < total_Textures_Num; i++)
        {
            if (i >= textures.Count)
                photos.Add(null);
            else
                photos.Add(textures[i]);
        }
    }

    public void AddPhoto(Texture2D texture)
    {
        //todo : add하면서 색깔을 다시 원상복귀해야함
        photos.Add(texture);
        lastPhotoIndex++;
        maxBookPlaneIndex = (photos.Count / 4) - 1;
        //늘어나기
    }
    public void RemovePhoto(Texture2D texture)
    {
        photos.Remove(texture);
        lastPhotoIndex--;
        maxBookPlaneIndex = (photos.Count / 4) - 1;
        //땡겨오기
    }
}


