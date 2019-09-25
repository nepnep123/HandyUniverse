using UnityEngine;

public class SmoothMouseLook : MonoBehaviour {

	public Vector2 clampInDegrees = new Vector2(360, 180);
	public bool lockCursor;
	public Vector2 sensitivity = new Vector2(2, 2);
	public Vector2 smoothing = new Vector2(3, 3);
	public Vector2 targetDirection;

	private Vector2 mouseAbsolute;
	private Vector2 smoothMouse;


	void Start() {
		targetDirection = transform.localRotation.eulerAngles;
	}

	void Update() {
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
		}
		Quaternion targetOrientation = Quaternion.Euler(targetDirection);
		Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
		smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
		smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseDelta.y, 1f / smoothing.y);
		mouseAbsolute += smoothMouse;
		if (clampInDegrees.x < 360) {
			mouseAbsolute.x = Mathf.Clamp (mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);
		}
		if (clampInDegrees.y < 360) {
			mouseAbsolute.y = Mathf.Clamp (mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);
		}
		transform.localRotation = Quaternion.AngleAxis(-mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;
		Quaternion yRotation = Quaternion.AngleAxis(mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
		transform.localRotation *= yRotation;
	}
}
