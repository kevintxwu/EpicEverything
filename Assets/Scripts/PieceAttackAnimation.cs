using UnityEngine;
using System.Collections;

public class PieceAttackAnimation : MonoBehaviour {

    private PieceController piece;
    private ArrowController arrow;

    public void DestroyArrow() {
        if (arrow != null) Destroy(arrow.gameObject);
        arrow = null;
    }

    void Awake() {
        piece = gameObject.GetComponent<PieceController>();
    }

    void FixedUpdate() {
        if (arrow != null) {
            arrow.UpdateTransform(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void OnMouseUp() {
        if (!piece.CanAttack()) {
            DestroyArrow();
            return;
        }
		piece.attacking = false;
		piece.HideSelect();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool destroyArrow = true;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Util.RayDepth, Util.PieceLayer)) {
            PieceController other = hit.collider.gameObject.GetComponent<PieceController>();
            if (other.player != piece.player && piece.InRange(other)) {
                StartCoroutine(AttackAfterSnap(other));
                destroyArrow = false;
            }
        }
        if (destroyArrow) DestroyArrow();
    }

    void OnMouseDown() {
        if (piece.cardState != null && piece.CanAttack()) {
			CreateArrow();
			piece.attacking = true;
			piece.ShowSelect();
		}
    }

    void CreateArrow() {
        arrow = ArrowController.Create(transform.position);
    }

    IEnumerator AttackAfterSnap(PieceController other) {
        arrow.Snap(other.cardState == null);
        yield return new WaitForSeconds(5 * Util.ArrowSnapWait);
        piece.Attack(other);
    }
}
