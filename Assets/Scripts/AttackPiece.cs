using UnityEngine;
using System.Collections;

public class AttackPiece : MonoBehaviour {

    private PieceController piece;
    private ArrowController arrow;
    private int rayDepth = 100;

    void Awake() {
        piece = gameObject.GetComponent<PieceController>();
    }

    void OnMouseUp() {
        Destroy(arrow.gameObject);
        if (piece.cardState == null ||
                piece.cardState.time + piece.lastAttackTime > Time.time) {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, rayDepth, Util.PieceLayer)) {
            PieceController otherPiece = hit.collider.gameObject.GetComponent<PieceController>();
            if (otherPiece.player != piece.player) piece.Attack(otherPiece);
        }
    }

    void OnMouseDown() {
        arrow = ArrowController.Create(transform.position);
    }
}
