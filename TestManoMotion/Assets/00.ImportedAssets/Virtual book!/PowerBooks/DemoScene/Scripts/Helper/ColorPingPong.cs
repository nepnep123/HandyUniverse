using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPingPong : MonoBehaviour {

	public Material mat;
	public Color colorA;
	public Color colorB;


	void Update () {
		mat.SetColor ("_TintColor", Color.Lerp (colorA, colorB, Mathf.PingPong (Time.time / 30f, 1)));
	}
}
