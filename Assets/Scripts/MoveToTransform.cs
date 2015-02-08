using UnityEngine;
using System.Collections;

public class MoveToTransform : MonoBehaviour {

	public Vector3 position;
	public Quaternion rotation;

	private bool moving;
	private float velocity;
	private float angularVelocity;
	private float duration;
	private float elapsedTime;
	
	private Vector3 targetPosition;
	private Quaternion targetRotation;
	private Vector3 currentVelocity;

	public void Move(Vector3 position, Quaternion rotation, float duration) {
		moving = true;
		targetPosition = position;
		targetRotation = rotation;
		velocity = Vector3.Distance(position, gameObject.transform.position) / duration;
		angularVelocity = Quaternion.Angle(rotation, gameObject.transform.rotation) / duration;
		this.position = position;
		this.rotation = rotation;
		this.duration = duration;
		currentVelocity = Vector3.zero;
	}
	
	void FixedUpdate() {
		if (moving) {
			if (transform.position == position && transform.rotation == rotation) {
				moving = false;
				return;
			}
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, duration);
			
//			transform.position = Vector3.MoveTowards(transform.position, position, step);
			float angularStep = angularVelocity * Time.deltaTime;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, angularStep);
		}
	}
}
