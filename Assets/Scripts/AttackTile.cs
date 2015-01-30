using UnityEngine;
using System.Collections;

public class AttackTile : MonoBehaviour {

	const int tileLayer = 1 << 8;

	private TileController tileController;
	private int rayDepth = 10;
	
	void Awake() {
		tileController = gameObject.GetComponent<TileController>();
	}
	
	void OnMouseUp() {
		print("Mouseup");
		print(tileController.cardState);
		if (tileController.cardState == null ||
				tileController.cardState.time + tileController.lastAttackTime > Time.time) {
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit, rayDepth, tileLayer)) {
			print("hit");
			TileController tile = hit.collider.gameObject.GetComponent<TileController>();
			if (tile.playerController != tileController.playerController) tileController.Attack(tile);
		}
		else {
			print("miss");
		}
	}
	
	void OnMouseDown() {
		print("Mousedown");
	}
}
