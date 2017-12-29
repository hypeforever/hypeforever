using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyController : MonoBehaviour
{
	public Transform cameraT;
	public Transform cameraTarget;

	public float startTransparencyDistance = 0.5f;
	public float endTransparencyDistance = 0.1f;
	public float maximumTransparency = 0.5f;

	private SkinnedMeshRenderer meshRenderer;

	// Use this for initialization
	public void Start ()
	{
		meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
		setupDepthBuffer ();
	}

	private void setupDepthBuffer()
	{
		foreach (Material material in meshRenderer.sharedMaterials)
		{
			material.SetInt ("_ZWrite", 1);
		}
	}
	
	// Update is called once per frame
	public void Update ()
	{
		float distance = Vector3.Distance (cameraTarget.position, cameraT.position);
		float opacity = 1;
		if (distance <= startTransparencyDistance)
		{
			float percentageInRange = (distance - endTransparencyDistance) / (startTransparencyDistance - endTransparencyDistance);
			opacity = Mathf.Lerp (maximumTransparency, 1, percentageInRange);
		}

		foreach (Material material in meshRenderer.sharedMaterials) 
		{
			Color color = material.color;
			color.a = opacity;
			material.color = color;
		}
	}
}
