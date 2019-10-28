using System.Collections;
using UnityEngine;

public class MKEnemySpawnManager : MonoBehaviour
{
    public GameObject meteorPrefab;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(1f);

    private void Start()
    {
        MKManager.instance.OnPlanetCreated += StartSpawn;
    }

    private void OnDisable()
    {
        MKManager.instance.OnPlanetCreated -= StartSpawn;
    }

    private void StartSpawn()
    {
        StartCoroutine(DelayMeteor());
    }
    
    public IEnumerator DelayMeteor()
    {
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(SpawnMeteor());
    }


    public IEnumerator SpawnMeteor()
    {
        Instantiate(meteorPrefab, RandomSphereInPoint(0.7f), Quaternion.identity);

        yield return waitForSeconds;

        StartCoroutine(SpawnMeteor());
    }

    public Vector3 RandomSphereInPoint(float radius)
    {
        Vector3 randomPos = Random.onUnitSphere;

        float r = Random.Range(0f, radius);
        //return (randomPos * r) + MKManager.instance.planet.transform.position;
        return (randomPos * r) + MKManager.instance.planet.transform.GetChild(0).transform.GetChild(0).transform.position;
    }
}
