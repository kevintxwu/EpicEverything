using UnityEngine;
using System.Collections;

public class HighlightPiece : MonoBehaviour {

    private PlayerController player;

    // void Start() {
    //     player = gameObject.GetComponent<PlayerController>();
    // }

    // void Update () {
    //     CardController card = player.selectedCard;
    //     if (card == null) {
    //         player.UnhighlightPiece();
    //         return;
    //     }
    //     RaycastHit hit = new RaycastHit();
    //     if (Physics.Raycast(card.gameObject.transform.position,
    //                         Vector3.down,
    //                         out hit,
    //                         Util.RayDepth,
    //                         Util.PieceLayer)) {
    //         PieceController piece = hit.collider.gameObject.GetComponent<PieceController>();
    //         if (piece == player.highlightedPiece ||
    //             piece.player != player) return;
    //         player.HighlightPiece(piece);
    //     } else player.UnhighlightPiece();
    // }
}
