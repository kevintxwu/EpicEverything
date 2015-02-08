using UnityEngine;
using System.Collections;

public class TileDamageAnimation : MonoBehaviour {

	private Vector3 defaultPosition;
	private Quaternion defaultRotation;
	private float endAnimationTime;
	private ParticleSystem rockParticle;
	private ParticleSystem sparkParticle;
	private ParticleSystem flashParticle;
	private ParticleSystem dustParticle;
	private ParticleSystem impactParticle;
	
//	public int damage;
	
	public void Animate(int damage, TileController other) {
		Vector3 positionDiff = transform.position - other.transform.position;
		positionDiff.y = 0;
		positionDiff.Normalize();
		float angle = Mathf.Atan(positionDiff.x / positionDiff.z) * Mathf.Rad2Deg;
		if (positionDiff.z >= 0) angle += 180;
		damage = Mathf.Min(10, damage);

		rockParticle.transform.rotation = Quaternion.Euler(-115, angle, 0);
		rockParticle.startSize = 5 + damage / 2;
		rockParticle.Emit(damage + 15);
		sparkParticle.transform.rotation = Quaternion.Euler(-140, angle, 0);
		sparkParticle.Emit(damage * 10 + 50);
		flashParticle.startSize = Mathf.Pow(damage, 2);
		flashParticle.Emit(damage * Mathf.Min(10, damage));
		impactParticle.Emit(damage * 100);
		dustParticle.Emit((int)Mathf.Pow(damage, 2) * 20);
		
		rigidbody.isKinematic = false;
		Vector3 force = new Vector3 (0, damage * 200 + 1000, 0);
		Vector3 position = new Vector3 (transform.position.x - positionDiff.x, 
		                                0, 
		                                transform.position.z - positionDiff.z);
		rigidbody.AddForceAtPosition(force, position);
		endAnimationTime = Time.time + 3;
	}
	
	void Update() {
		if (transform.position.y > 20) transform.position = new Vector3(transform.position.x, 20, transform.position.z);
		Vector3 angles = transform.rotation.eulerAngles;
		if (angles.x > 300 || angles.x < 240)
			transform.rotation = Quaternion.Euler(Mathf.Clamp(angles.x, 240, 300), angles.y, angles.z);
		if (Time.time > endAnimationTime) ResetTransform();
	}
	
	void Start() {
		defaultPosition = transform.position;
		defaultRotation = transform.rotation;
		rockParticle = transform.Find("RockParticle").particleSystem;
		sparkParticle = transform.Find("SparkParticle").particleSystem;
		flashParticle = transform.Find("FlashParticle").particleSystem;
		dustParticle = transform.Find("DustParticle").particleSystem;
		impactParticle = transform.Find("ImpactParticle").particleSystem;
	}
	
	private void ResetTransform() {
		rigidbody.isKinematic = true;
		transform.position = defaultPosition;
		transform.rotation = defaultRotation;
	}
}
