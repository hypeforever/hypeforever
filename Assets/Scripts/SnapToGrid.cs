using UnityEngine;

// Source: http://www.alanzucconi.com/2015/07/22/how-to-snap-to-grid-in-unity3d/

// This script is executed in the editor
[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour {
#if UNITY_EDITOR
	public float snapValue = 0.5f;

	void Update () {
		if (snapValue > 0.0f) {
			transform.localPosition = RoundTransform(transform.localPosition, snapValue);
		}
	}

	// The snapping code
	private Vector3 RoundTransform(Vector3 v, float snapValue) {
		return new Vector3 (
			snapValue * Mathf.RoundToInt(v.x / snapValue),
			snapValue * Mathf.RoundToInt(v.y / snapValue),
			snapValue * Mathf.RoundToInt(v.z / snapValue)
		);
	}
#endif
}
