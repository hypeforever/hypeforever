using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float turnSmoothTime = 0.2f;
	public float speedSmoothTime = 0.1f;
	public float gravity = -12;
	public float jumpHeight = 1;
	public float airControlPercent = 0.1f;

	private float turnSmoothVelocity;
	private float speedSmoothVelocity;
	private float currentSpeed;
	private float velocityY;

	private Animator animator;
	private Transform cameraT;
	private CharacterController characterController;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		characterController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDirection = input.normalized;

		if (Input.GetKeyDown (KeyCode.Space)) {
			jump();
		}

		if (inputDirection != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, getModifiedSmoothTime(turnSmoothTime));
		}

		bool running = Input.GetKey (KeyCode.LeftShift);
		float targetSpeed = (running ? runSpeed : walkSpeed) * inputDirection.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, getModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;

		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
		characterController.Move (velocity * Time.deltaTime);
		currentSpeed = new Vector2 (characterController.velocity.x, characterController.velocity.z).magnitude;

		if (characterController.isGrounded) {
			velocityY = 0;
		}

		float animationSpeedPercent = running ? (currentSpeed / runSpeed) : (currentSpeed / walkSpeed * 0.5f);
		animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
	}

	private void jump()
	{
		if (!characterController.isGrounded) {
			return;
		}

		float jumpVelocity = Mathf.Sqrt (-2 * gravity * jumpHeight);
		velocityY = jumpVelocity;
	}

	private float getModifiedSmoothTime(float smoothTime)
	{
		if (characterController.isGrounded) {
			return smoothTime;
		}

		if (airControlPercent == 0) {
			return float.MaxValue;
		}
		
		return smoothTime / airControlPercent;
	}
}
