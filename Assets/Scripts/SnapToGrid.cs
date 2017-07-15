using UnityEngine;

// Inspired by: http://www.alanzucconi.com/2015/07/22/how-to-snap-to-grid-in-unity3d/

// This script is executed in the editor
[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour {
#if UNITY_EDITOR
	public bool snapPosition = true;
	public float snapPositionValue = 0.5f;

	public bool snapRotation = true;
	public float snapRotateValue = 90f;

	void Update () {
		if (snapPosition && snapPositionValue > 0.0f) {
			transform.localPosition = calculateSnapPosition(transform.localPosition);
		}

		if (snapRotation && snapRotateValue > 0.0f) {
			transform.localEulerAngles = calculateSnapRotation(transform.localEulerAngles);
		}
	}

	private Vector3 calculateSnapPosition(Vector3 v) {
		return new Vector3 (
			round(v.x, snapPositionValue),
			round(v.y, snapPositionValue),
			round(v.z, snapPositionValue)
		);
	}
	
	private Vector3 calculateSnapRotation(Vector3 eulerAngles) {
		return new Vector3 (
			round(eulerAngles.x, snapRotateValue),
			round(eulerAngles.y, snapRotateValue),
			round(eulerAngles.z, snapRotateValue)
		);
	}

	private float round(float angle, float snapValue) {
		return snapValue * Mathf.RoundToInt(angle / snapValue);
	}
#endif
}
