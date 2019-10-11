using UnityEngine;
using UnityEngine.UI;

public class KangWon_RecognizeGameObject : MonoBehaviour
{
    public static BoxCollider[] enemies;
    public static bool[] isDetecteds;

    private Camera thisCam;
    private Plane[] planes;

    private void Awake()
    {
        thisCam = GetComponent<Camera>();
        var es = GameObject.FindGameObjectsWithTag("ANIMALS");
        enemies = new BoxCollider[es.Length];

        isDetecteds = new bool[es.Length];

        for (int i = 0; i < es.Length; i++)
        {
            enemies[i] = es[i].GetComponent<BoxCollider>();
            isDetecteds[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //아래의 코드는 카메라의 Frustum 영역의 각 면에 해당하는 plane을 반환해줌.
        //6개의 플레인 : 위,아래,좌,우,가까운,먼 플레인들을 반환함.
        planes = GeometryUtility.CalculateFrustumPlanes(thisCam);
        //에너미 숫자만큼 실행
        for (int i = 0; i < enemies.Length; i++)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, enemies[i].transform.position - transform.position);
            //적이 플레인 안에 있다면?
            //위에서 구한 플레인 안에 콜라이더의 바운드들이 있다면 true를 반환하는 코드.
            if (GeometryUtility.TestPlanesAABB(planes, enemies[i].bounds) /*&& Physics.Raycast(ray, out hit, 300f) &&
                hit.collider.CompareTag("ANIMALS")*/)
            {
                if (isDetecteds[i] == true)
                    return;

                Debug.Log(i + " 동물 발견");
                isDetecteds[i] = true; //i번째 찾았다.
            }
            //else
            //{
            //    isDetecteds[i] = false;
            //}
        }
    }
}
