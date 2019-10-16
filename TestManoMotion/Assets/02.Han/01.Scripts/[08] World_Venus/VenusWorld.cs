using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusWorld : World
{
    public Color venusFog = new Color(0.854f, 0.694f, 0.004f);
    VenusSfx sfxs;
    // Start is called before the first frame update
    void Start()
    {
        SetVenusFog();
    }

    private void SetVenusFog()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = venusFog;
        RenderSettings.fogDensity = 0.4f;
    }

    bool isInit = false;

    private void Update()
    {
        if (isInit == true) return;
        BackGroundOff();
        isInit = true;
    }
    void BackGroundOff()
    {
        var manov = FindObjectOfType<ManoVisualization>();
        manov.Show_background_layer = false;
        manov._layer_background.enabled = false;
        manov._layer_background.gameObject.SetActive(false);
    }
}
