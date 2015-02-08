using UnityEngine;
using System.Collections;

public class TileDamageAnimation : MonoBehaviour {

	private ParticleSystem rockParticle;
	private ParticleSystem sparkParticle;
	private ParticleSystem flashParticle;
	private ParticleSystem dustParticle;
	private ParticleSystem impactParticle;
	
	
	public void Animate(int damage, TileController other) {
		Vector3 positionDiff = transform.position - other.transform.position;
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
		
//		Vector3 force = new Vector3 (0, damage * 200 + 1000, 0);
//		Vector3 position = new Vector3 (transform.position.x + angle, 
//		                                transform.position.y, 
//		                                transform.position.z - 1);
//		rigidbody.AddForceAtPosition(force, position);
	}
	
	void Start() {
		rockParticle = transform.Find("RockParticle").particleSystem;
		sparkParticle = transform.Find("SparkParticle").particleSystem;
		flashParticle = transform.Find("FlashParticle").particleSystem;
		dustParticle = transform.Find("DustParticle").particleSystem;
		impactParticle = transform.Find("ImpactParticle").particleSystem;
	}
}
