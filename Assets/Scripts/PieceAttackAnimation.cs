using UnityEngine;
using System.Collections;

public class PieceAttackAnimation : MonoBehaviour {

    private PieceController piece;
    private ArrowController arrow;
    private PieceController otherPiece;

    public void DestroyArrow() {
        if (arrow != null) Destroy(arrow.gameObject);
        arrow = null;
    }

    public void Attack() {
        piece.Attack(otherPiece);
    }

    void Awake() {
        piece = gameObject.GetComponent<PieceController>();
    }

    void OnMouseUp() {
        if (piece.cardState == null ||
            piece.cardState.time + piece.lastAttackTime > Time.time) {
            DestroyArrow();
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        bool destroyArrow = true;
        if (Physics.Raycast(ray, out hit, Util.RayDepth, Util.PieceLayer)) {
            otherPiece = hit.collider.gameObject.GetComponent<PieceController>();
            if (otherPiece.player != piece.player) {
                arrow.Snap(otherPiece.cardState == null);
                destroyArrow = false;
            }
        }
        if (destroyArrow) DestroyArrow();
    }

    void OnMouseDown() {
        if (piece.cardState != null) CreateArrow();
    }

    void CreateArrow() {
        arrow = ArrowController.Create(transform.position, this);
    }
}
