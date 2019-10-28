using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 행성 생성
public class MKManager : MonoBehaviour
{
    public static MKManager instance;

    private GameObject MoveObject = null;
    public float RotateSpeed = 5f;
    public float ZoomSpeed = 5f;
    private float OriginScale;
    private float FirstScale;
    private GameObject panelTouch;

    public GameObject planet;
    private GameObject buttonGeneratePlanet;
    private GameObject panelGameOver;
    private GameObject panelPause;
    private GameObject buttonPause;
    private Text textScoreResult;       // 출력 점수
    private Text textHighScoreResult;   // 출력 최고 점수

    [HideInInspector]
    public SphereCollider planetSphereCollider;

    Vector3 transformPos = new Vector3(0, 0, 0.4f);

    private WaitForSeconds waitForSeconds = new WaitForSeconds(3.5f);

    public VoidNotier OnPlanetCreated;

    private bool viewMode;

    private void Awake()
    {
        if (instance == null) instance = GetComponent<MKManager>();
        else Destroy(instance);
    }

    private void Start()
    {
        buttonGeneratePlanet = GameObject.Find("ButtonGeneratePlanet");
        panelGameOver = GameObject.Find("PanelGameOver");
        panelPause = GameObject.Find("PanelPause");
        buttonPause = GameObject.Find("ButtonPause");
        textScoreResult = GameObject.Find("TextScoreResult").GetComponent<Text>();
        textHighScoreResult = GameObject.Find("TextHighScoreResult").GetComponent<Text>();

        panelTouch = GameObject.Find("PanelTouch");

        panelGameOver.SetActive(false);
        panelPause.SetActive(false);
    }

    private void Update()
    {
        ARControl();
    }

    public void SetARObject(GameObject ARObject)
    {
        MoveObject = ARObject;

        ARObject.GetComponent<MKPlanetLeftRight>().enabled = false;
        ARObject.transform.GetChild(0).transform.GetComponent<MKLookAt>().enabled = false;
        ARObject.transform.GetChild(0).transform.GetChild(0).GetComponent<MKPlanetUpDown>().enabled = false;

        FirstScale = 1;
        OriginScale = MoveObject.transform.localScale.x;
    }

    private void ARControl()
    {
        if (MoveObject == null)
        {
            return;
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float XaxisRotation = Input.GetAxis("Mouse X") * RotateSpeed;
                float YaxisRotation = Input.GetAxis("Mouse Y") * RotateSpeed;
                MoveObject.transform.Rotate(YaxisRotation, -XaxisRotation, 0, Space.World);
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                touch.deltaPosition = Vector2.zero;
            }

        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1Distance = touch1.position - touch1.deltaPosition;
            Vector2 touch2Distance = touch2.position - touch2.deltaPosition;

            float preTouchDeltaMag = (touch1Distance - touch2Distance).magnitude;
            float touchDeltaMag = (touch1.position - touch2.position).magnitude;


            float deltamagnitude = preTouchDeltaMag - touchDeltaMag;

            float pinchMount = FirstScale - (deltamagnitude * (ZoomSpeed / 100));

            pinchMount = Mathf.Clamp(pinchMount, 0.5f, 2.0f);

            FirstScale = Mathf.Lerp(FirstScale, pinchMount, 5 * Time.deltaTime);

            MoveObject.transform.localScale = new Vector3(OriginScale * FirstScale, OriginScale * FirstScale, OriginScale * FirstScale);

            if (touch1.phase == TouchPhase.Ended && touch2.phase == TouchPhase.Ended)
            {
                touch1.deltaPosition = Vector2.zero;
                touch2.deltaPosition = Vector2.zero;
            }
        }
    }

    public void MakePlanet()
    {
        var planetObj = Instantiate(planet); // 생성
        planet = planetObj; // 딱 한번만 생성 = 다시는 생성 안됨
        planetObj.transform.SetParent(Camera.main.transform.parent.transform); // 차일드화
        planetObj.transform.localPosition = transformPos;
        planetSphereCollider = planet.GetComponentInChildren<SphereCollider>();
        buttonGeneratePlanet.SetActive(false);

        var player = GameObject.FindWithTag("Player").gameObject;
        player.SetActive(false); // 차 안보이게 하기

        SetARObject(planetObj);

        if (viewMode == true)
        {
            //panelTouch.SetActive(false);

            OnPlanetCreated?.Invoke(); // 방송
            // = 
            //if(OnPlanetCreated != null)
            //{
            //    OnPlanetCreated();
            //}
        }
    }

    public void EndGame() // 게임끝 -> 패널 짠
    {
        //TextDistanceScoreResult.text = "NOW SCORE : " + Mathf.Round(PlayerPrefs.GetFloat("NowScore")) + "M";
        //TextHighDistanceScoreResult.text = "HIGH SCORE : " + Mathf.Round(PlayerPrefs.GetFloat("HighScore")) + "M";
        panelGameOver.SetActive(true);
    }

    public void PauseGame() // 일시정지
    {
        panelPause.SetActive(true);
        buttonPause.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame() // 일시정지 해제
    {
        panelPause.SetActive(false);
        buttonPause.SetActive(true);
        Time.timeScale = 1f;
    }

    //public void Restart() // 씬 리스타트.. 이거 수정해야
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    public void QuitGame() // 게임종료
    {
        Application.Quit();
    }
}