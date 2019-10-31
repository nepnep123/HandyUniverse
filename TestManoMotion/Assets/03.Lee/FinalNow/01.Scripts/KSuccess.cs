using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSuccess : MonoBehaviour
{
    public ParticleSystem a1;
    public ParticleSystem a2;

    private void OnEnable()
    {
        StartCoroutine(StartParticle());
    }

    IEnumerator StartParticle()
    {
        a1.Play();
        a2.Play();
        yield return new WaitForSeconds(1.5f);
        a1.Stop();
        a2.Stop();
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(StartParticle());
    }
}
