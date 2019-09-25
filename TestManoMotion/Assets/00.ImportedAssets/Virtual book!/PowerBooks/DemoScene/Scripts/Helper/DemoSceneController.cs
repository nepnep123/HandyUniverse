using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TLGFPowerBooks;

public class DemoSceneController : MonoBehaviour {

	public PBook demoBook1;
	public ExampleUIController exampleUIController;
	public Camera mainCam;
	public Camera overlayCam;
	public Transform[] camWaypoints;
	public int camWaypointIndex = 0;
	public GameObject[] stepTexts;
	public Text nextStepText;

	private PBook overlayPowerBook = null;
	private Vector3 overlayBookOriginalPos;
	private Quaternion overlayBookOriginalRot;
	private bool rotateAround = true;


	// Use this for initialization
	void Start () {
		mainCam.transform.position = camWaypoints [camWaypointIndex].position;
		mainCam.transform.rotation = camWaypoints [camWaypointIndex].rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (camWaypointIndex == 0 && rotateAround) {
			mainCam.transform.RotateAround (Vector3.zero, Vector3.up, 10 * Time.deltaTime);
		}

		if (camWaypointIndex == 2 || camWaypointIndex == 4) {
			if (overlayPowerBook != null && overlayPowerBook.GetBookState () == PBook.BookState.CLOSED) {
				if (camWaypointIndex == 2) {
					stepTexts [camWaypointIndex].SetActive (true);
					overlayPowerBook.transform.position = overlayBookOriginalPos;
					overlayPowerBook.transform.rotation = overlayBookOriginalRot;
					overlayCam.gameObject.SetActive (false);
				}
				if (camWaypointIndex == 4) {
					stepTexts [camWaypointIndex].SetActive (true);
				}
				exampleUIController.gameObject.SetActive (false);
				exampleUIController.pBook = null;
				overlayPowerBook = null;
			}

			if (Input.GetMouseButtonDown (0) && overlayPowerBook == null) {
				RaycastHit hit;
				Ray ray = mainCam.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) {
					if (hit.transform.parent.GetComponent<PBook> () != null) {
						overlayPowerBook = hit.transform.parent.GetComponent<PBook> ();
						if (camWaypointIndex == 2) {
							BoxCollider bookCollider = (BoxCollider)hit.collider;
							overlayBookOriginalPos = hit.transform.position;
							overlayBookOriginalRot = hit.transform.rotation;
							overlayPowerBook.transform.position = new Vector3 ((bookCollider.size.y / 2) - 0.005f, 0, 9.875f);
							overlayPowerBook.transform.rotation = Quaternion.identity;
							overlayCam.gameObject.SetActive (true);
						}
						overlayPowerBook.OpenBook ();
						exampleUIController.gameObject.SetActive (true);
						exampleUIController.pBook = overlayPowerBook;
						stepTexts [camWaypointIndex].SetActive (false);
					}
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			NextWaypoint ();
		}
	}

	public void NextWaypoint () {
		StopCoroutine ("NextWaypointAnim");
		nextStepText.text = "Next Step";
		camWaypointIndex++;
		if (camWaypointIndex >= camWaypoints.Length) {
			camWaypointIndex = 0;
			nextStepText.text = "Start";
		}
		if (exampleUIController.pBook != null && exampleUIController.pBook.GetBookState () != PBook.BookState.CLOSED) {
			exampleUIController.pBook.CloseBook ();
		}
		exampleUIController.gameObject.SetActive (false);
		foreach (GameObject go in stepTexts) {
			go.SetActive (false);
		}
		stepTexts [camWaypointIndex].SetActive (true);
		if (camWaypointIndex == 0) {
			overlayPowerBook = null;
		}
		if (camWaypointIndex == 1) {
			exampleUIController.pBook = demoBook1;
			exampleUIController.gameObject.SetActive (true);
		}
		if (camWaypointIndex == 3) {
			if (overlayPowerBook != null) {
				overlayPowerBook.transform.position = overlayBookOriginalPos;
				overlayPowerBook.transform.rotation = overlayBookOriginalRot;
				overlayCam.gameObject.SetActive (false);
			}
		}
		StartCoroutine ("NextWaypointAnim");
	}

	IEnumerator NextWaypointAnim () {
		rotateAround = false;
		for (;;) {
			mainCam.transform.position = Vector3.Lerp (mainCam.transform.position, camWaypoints[camWaypointIndex].position, Time.deltaTime * 2.5f);
			mainCam.transform.rotation = Quaternion.Lerp (mainCam.transform.rotation, camWaypoints[camWaypointIndex].rotation, Time.deltaTime * 2.0f);
			if (Vector3.Distance(mainCam.transform.position, camWaypoints [camWaypointIndex].position) < 0.02f && Quaternion.Angle(mainCam.transform.rotation, camWaypoints[camWaypointIndex].rotation) < 0.5f) {
				break;
			}
			yield return null;
		}
		rotateAround = true;
	}
}
