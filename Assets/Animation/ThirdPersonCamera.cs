using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	public float mouseSensitivity = 10;
	public Transform target;
	public float distanceFromTarget = 2;
	public Vector2 pitchBounds = new Vector2 (-40, 85);

	public float rotationSmoothTime = 0.12f;
	public float sphereCastThickness = 0.2f;

	public bool cursorLocked = true;

	public LayerMask CamOcclusion;

	private Vector3 rotationSmoothVelocity;
	private Vector3 currentRotation;

	private float yaw;
	private float pitch;

	public void Start()
	{
		lockCursor();
	}

	private void lockCursor()
	{
		if (cursorLocked)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	
	public void LateUpdate()
	{
		handleRotation();
		calculatePosition();
	}

	private void handleRotation()
	{
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp (pitch, pitchBounds.x, pitchBounds.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;
	}

	private void calculatePosition()
	{
		transform.position = getCameraPosition(getUnobstructedDistanceFromTargetToCamera());
	}

	private Vector3 getCameraPosition(float distance)
	{
		return target.position + (getDirectionFromTargetToCamera() * distance);
	}

	private Vector3 getDirectionFromTargetToCamera()
	{
		return transform.forward * -1;
	}

	private float getUnobstructedDistanceFromTargetToCamera()
	{
		RaycastHit hit = new RaycastHit();
		if (Physics.SphereCast(target.position, sphereCastThickness, getDirectionFromTargetToCamera(), out hit, distanceFromTarget, CamOcclusion))
		{
			return hit.distance;
		}

		return distanceFromTarget;
	}
}
