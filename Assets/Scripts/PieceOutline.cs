using UnityEngine;
using System.Collections;

public class PieceOutline : MonoBehaviour {

    private PieceController piece;

    void Start() {
        piece = gameObject.GetComponent<PieceController>();
    }

	void Update() {
		if (piece.Ready()) {
			piece.ShowOutline();
		} else {
			piece.HideOutline();
		}
	}

    void OnMouseOver() {
		if (!Util.CheckPlayer(piece.player)) return;
        if (piece.cardState != null && piece.Ready()) {
			piece.ShowSelect();
		}
    }

	void OnMouseExit() {
		if (!piece.attacking) piece.HideSelect();
	}
}
