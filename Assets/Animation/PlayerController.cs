using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
	void Start ()
	{
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		characterController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		handleRotation ();
		handleMovement ();
		handleJump ();
		handleAnimation ();
	}

	private Vector2 getInputDirection ()
	{
		float inputX = Input.GetAxisRaw ("Horizontal");
		float inputY = Input.GetAxisRaw ("Vertical");
		Vector2 input = new Vector2 (inputX, inputY);
		return input.normalized;
	}

	private void handleRotation ()
	{
		if (!isInput ()) {
			return;
		}
		Vector2 inputDirection = getInputDirection ();
		float targetRotation = Mathf.Atan2 (inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
		transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle (transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, getModifiedSmoothTime (turnSmoothTime));
	}

	private bool isInput ()
	{
		return getInputDirection () != Vector2.zero;
	}

	private float getTargetSpeed ()
	{
		float inputSpeed = getInputDirection ().magnitude;
		if (isRunning ()) {
			return runSpeed * inputSpeed;
		} else {
			return walkSpeed * inputSpeed;
		}
	}

	private bool isRunning ()
	{
		return Input.GetKey (KeyCode.LeftShift);
	}

	private void handleMovement ()
	{
		float targetSpeed = getTargetSpeed ();
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, getModifiedSmoothTime (speedSmoothTime));
		characterController.Move (transform.forward * currentSpeed * Time.deltaTime);
		currentSpeed = new Vector2 (characterController.velocity.x, characterController.velocity.z).magnitude;
	}

	private void handleJump ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			jump ();
		}

		velocityY += Time.deltaTime * gravity;

		Vector3 velocity = Vector3.up * velocityY;
		characterController.Move (velocity * Time.deltaTime);

		if (characterController.isGrounded) {
			velocityY = 0;
		}
	}

	private void jump ()
	{
		if (!characterController.isGrounded) {
			return;
		}

		float jumpVelocity = Mathf.Sqrt (-2 * gravity * jumpHeight);
		velocityY = jumpVelocity;
	}

	private float getModifiedSmoothTime (float smoothTime)
	{
		if (characterController.isGrounded) {
			return smoothTime;
		}

		if (airControlPercent == 0) {
			return float.MaxValue;
		}
		
		return smoothTime / airControlPercent;
	}

	private void handleAnimation ()
	{
		float animationSpeedPercent = isRunning () ? (currentSpeed / runSpeed) : (currentSpeed / walkSpeed * 0.5f);
		animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
	}
}
