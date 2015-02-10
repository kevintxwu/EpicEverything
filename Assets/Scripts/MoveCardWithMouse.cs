using UnityEngine;
using System.Collections;

public class MoveCardWithMouse : MonoBehaviour {

    private CardController card;
    private Vector3 position;

    void Start () {
        card = gameObject.GetComponent<CardController>();
        position = transform.position;
    }

    void Update () {
        if (card.selected) {
            Vector3 cameraPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 oldPosition = position;
            position = new Vector3 (cameraPoint.x, Util.CardHeight, cameraPoint.z);
            Vector3 velocity = position - oldPosition;
            transform.position = position;
        }
    }

    void OnMouseDown() {
        card.PickupCard();
    }

    void OnMouseUp () {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(
                transform.position, Vector3.down, out hit, Util.RayDepth, Util.PieceLayer)) {
            PieceController piece = hit.collider.gameObject.GetComponent<PieceController>();
            if (card.IsPlayable(piece)) {
                card.PlayCard(piece);
                return;
            }
        }
        card.ReturnToHand();
    }

}
