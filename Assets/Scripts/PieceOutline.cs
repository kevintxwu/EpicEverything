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

    void OnMouseEnter() {
        if (piece.cardState != null) piece.ShowOutline(); 
    }

    void OnMouseExit() {
        piece.HideOutline();
    }
}
