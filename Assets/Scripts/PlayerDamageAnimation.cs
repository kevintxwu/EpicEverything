using UnityEngine;
using System.Collections;

public class PlayerDamageAnimation : MonoBehaviour {

	public void Animate(int damage) {
		print("PlayerDeathAnimation: " + gameObject.name + " damage=" + damage);
	}

}
