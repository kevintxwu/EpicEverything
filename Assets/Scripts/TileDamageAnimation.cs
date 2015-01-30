using UnityEngine;
using System.Collections;

public class TileDamageAnimation : MonoBehaviour {

	private ParticleSystem backParticle;
	private ParticleEmitter smokeParticle;

	public void Animate(int damage, TileController other) {
		Vector3 positionDiff = transform.position - other.transform.position;
		float angle = Mathf.Atan(positionDiff.x / positionDiff.z) * Mathf.Rad2Deg;
		print(angle);
		if (positionDiff.z <= 0) angle += 180;
		backParticle.transform.rotation = Quaternion.Euler(0, angle, 0);
		backParticle.Emit(100);
		smokeParticle.Emit(100);
		print("TileDamageAnimation: " + gameObject.name + " damage=" + damage);
	}
	
	void Awake() {
		backParticle = transform.Find("BackParticle").particleSystem;
		smokeParticle = transform.Find("SmokeParticle").particleEmitter;
	}
}
