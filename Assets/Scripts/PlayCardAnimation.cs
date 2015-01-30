using UnityEngine;
using System.Collections;

public class PlayCardAnimation : MonoBehaviour {
	public void Animate(CardState cardState) {
		renderer.material = cardState.tileMaterial;
	}
}
