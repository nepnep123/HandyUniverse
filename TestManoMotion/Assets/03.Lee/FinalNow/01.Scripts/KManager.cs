using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KManager : MonoBehaviour
{
    public static KManager instance;

    public GameObject[] planetClick;
    private int count = 0;

    private GameObject MoveObject = null;
    public float RotateSpeed = 5f;
    public float ZoomSpeed = 5f;
    private float OriginScale;
    private float FirstScale;

    public GameObject solar;

    private GameObject panelTouch;

    private void Awake()
    {
        if (instance == null) instance = GetComponent<KManager>();
        else Destroy(instance);
    }

    private void Start()
    {
        GameManager.instance.BackGroundOn(false);
        //BackGroundOff();
        panelTouch = GameObject.Find("PanelTouch"); // 칼리버레이션 하면 다시 true
    }

    public void solarGenerate()
    {
        var solarFiled = Instantiate(solar);
        solar = solarFiled;

        SetARObject(solarFiled);
        KTutorialMode.grablockdIs = false;
    }

    public void PlanetClicking()
    {
        //planetClick[count].
        Debug.Log(planetClick[count]);
        count++;
        if (count > planetClick.Length - 1)
        {
            count = 0;
        }
    }

    private void Update()
    {
        ARControl();
    }

    public void SetARObject(GameObject ARObject)
    {
        MoveObject = ARObject;
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


    // 제스쳐
    //private void Update()
    //{
    //    PlanetHandLeftRight(ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);
    //}

    //private void PlanetHandLeftRight(TrackingInfo tracking_info)
    //{
    //    float angle = Mathf.LerpAngle(transform.rotation.z, tracking_info.rotation, 0.8f);
    //    transform.eulerAngles = new Vector3(0, 0, -angle);
    //}
}