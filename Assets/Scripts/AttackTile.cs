using UnityEngine;
using System.Collections;

public class AttackTile : MonoBehaviour {

	const int tileLayer = 1 << 8;

	private TileController tileController;
	private ArrowController arrow;
	private int rayDepth = 100;
	
	void Awake() {
		tileController = gameObject.GetComponent<TileController>();
	}
	
	void OnMouseUp() {
		Destroy(arrow.gameObject);
		if (tileController.cardState == null ||
				tileController.cardState.time + tileController.lastAttackTime > Time.time) {
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit, rayDepth, tileLayer)) {
			TileController tile = hit.collider.gameObject.GetComponent<TileController>();
			if (tile.playerController != tileController.playerController) tileController.Attack(tile);
		}
	}
	
	void OnMouseDown() {
		arrow = ArrowController.Create(transform.position);
	}
}
