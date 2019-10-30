using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusWorld : World
{
    VenusSfx venusSounds;
    public Color venusFog = new Color(0.854f, 0.694f, 0.004f);
    public Helper helper;
    // Start is called before the first frame update

    public override void InitWorld()
    {
        GameManager.instance.hand.mode = GameManager.instance.hand.venusMode;
        var a = GetComponentsInChildren<InteractableObject>(true);
        foreach(InteractableObject aa in a)
        {
            aa.ProcessInit(helper);
        }
        SetVenusFog();
        venusSounds = GetComponentInChildren<VenusSfx>();
        venusSounds.InitVenusSounds();
        //Todo 테스트용
#if !UNITY_EDITOR
        BackGroundOff();
#endif
        PhotoUtils.MakeFolder("Venus_Lakshmi");
        PhotoUtils.MakeFolder("Venus_Maxwell");
        PhotoUtils.MakeFolder("Venus_Venus");
    }

    private void SetVenusFog()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = venusFog;
        RenderSettings.fogDensity = 0.4f;
    }

    void BackGroundOff()
    {
        var manov = FindObjectOfType<ManoVisualization>();
        manov.Show_background_layer = false;
        manov._layer_background.enabled = false;
        manov._layer_background.gameObject.SetActive(false);
    }
}
