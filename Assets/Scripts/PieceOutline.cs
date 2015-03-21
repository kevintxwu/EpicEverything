using UnityEngine;
using System.Collections;

public class PieceOutline : MonoBehaviour {

    private PieceController piece;

    void Start() {
        piece = gameObject.GetComponent<PieceController>();
    }

    // void Update() {
    //     RaycastHit hit = new RaycastHit();
    //     if (Physics.Raycast(card.gameObject.transform.position,
    //         Vector3.down, out hit, Util.RayDepth, Util.PieceLayer)) {
    //         PieceController piece = hit.collider.gameObject.GetComponent<PieceController>();
    //         player.HighlightPiece(piece);
    //     }
    // }

	void Update() {
		if (piece.CanAttack()) {
			piece.ShowOutline();
		} else {
			piece.HideOutline();
		}
	}

    void OnMouseOver() {
        if (piece.cardState != null && piece.CanAttack()) {
			piece.ShowSelect();
		}
    }

	void OnMouseExit() {
		if (!piece.attacking) piece.HideSelect();
	}
}
