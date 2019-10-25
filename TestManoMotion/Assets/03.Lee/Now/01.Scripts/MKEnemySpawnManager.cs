using System.Collections;
using UnityEngine;

// 3.5초 있다가 활성화 됨
public class MKEnemySpawnManager : MonoBehaviour
{
    public static MKEnemySpawnManager instance;

    public GameObject meteorPrefab;

    public Transform planet;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1f);

    void Awake()
    {
        instance = this;
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
        return (randomPos * r) + planet.position;
    }
}
