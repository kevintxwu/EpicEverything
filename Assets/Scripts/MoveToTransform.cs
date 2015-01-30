using UnityEngine;
using System.Collections;

public class MoveToTransform : MonoBehaviour {

	public Vector3 position;
	public Quaternion rotation;

	private bool moving;
	private float velocity;
	private float angularVelocity;

	public void Move(Vector3 position, Quaternion rotation, float time) {
		moving = true;
		velocity = Vector3.Distance(position, gameObject.transform.position) / time;
		angularVelocity = Quaternion.Angle(rotation, gameObject.transform.rotation) / time;
		this.position = position;
		this.rotation = rotation;
	}
	
	void Update () {
		if (moving) {
			if (transform.position == position && transform.rotation == rotation) {
				moving = false;
				return;
			}
			float step = velocity * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, position, step);
			float angularStep = angularVelocity * Time.deltaTime;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, angularStep);
		}
	}

}
