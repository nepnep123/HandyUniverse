using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusWorld : World
{
    public Color venusFog = new Color(0.854f, 0.694f, 0.004f);
    VenusSfx venusSounds;
    // Start is called before the first frame update

    bool isInit = false;

    private void Update()
    {
        if (isInit == true) return;
        InitWorld();
        isInit = true;
    }

    public void InitWorld()
    {
        BackGroundOff();
        SetVenusFog();
        venusSounds = GetComponentInChildren<VenusSfx>();
        venusSounds.InitVenusSounds();
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
