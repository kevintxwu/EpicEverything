using UnityEngine;
using System.Collections;

public class HighlightTile : MonoBehaviour {
	
	const int tileLayer = 1 << 8;
	
	public float rayDepth;
	
	private PlayerController playerController;
	
	void Start() {
		playerController = gameObject.GetComponent<PlayerController>();
	}
	
	void Update () {
		CardController card = playerController.selectedCard;
		if (card == null) {
			playerController.UnhighlightTile();
			return;
		}
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(card.gameObject.transform.position, Vector3.down, out hit, rayDepth, tileLayer)) {
			TileController tile = hit.collider.gameObject.GetComponent<TileController>();
			if (tile == playerController.highlightedTile) return;
			playerController.HighlightTile(tile);
		} else {
			playerController.UnhighlightTile();
		}
	}
}
