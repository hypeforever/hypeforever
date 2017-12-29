using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	public float mouseSensitivity = 10;
	public Transform target;
	public float distanceFromTarget = 2;
	public Vector2 pitchBounds = new Vector2 (-40, 85);

	public float rotationSmoothTime = 0.12f;

	public bool lockCursor = true;

	public LayerMask CamOcclusion;

	private Vector3 rotationSmoothVelocity;
	private Vector3 currentRotation;

	private float yaw;
	private float pitch;

	// Use this for initialization
	void Start () {
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp (pitch, pitchBounds.x, pitchBounds.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * distanceFromTarget;

		transform.position = raycast ();
		//transform.LookAt (target);
	}

	private Vector3 raycast()
	{
		RaycastHit hit = new RaycastHit ();

		if (Physics.Linecast (target.position, transform.position, out hit, CamOcclusion))
		{
			return hit.point + hit.normal * 0.1f;
		}

		return transform.position;
	}
}
