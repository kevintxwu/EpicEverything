using UnityEngine;
using System.Collections;
using TMPro;

public class MoveCardWithMouse : MonoBehaviour {

	const float fadeRate = 0.1f;
	const float fadeWait = 0.001f;

    private CardController card;
    private Vector3 position;

    void Start () {
        card = gameObject.GetComponent<CardController>();
        position = transform.position;
    }

    void Update () {
		if (!card.usable) return;
        if (card.selected) {
            Vector3 cameraPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            Vector3 oldPosition = position;
            position = new Vector3 (cameraPoint.x, Util.CardHeight, cameraPoint.z);
//            Vector3 velocity = position - oldPosition;
            transform.position = position;
        }
    }

    void OnMouseDown() {
		if (!card.usable) return;
		card.PickupCard();
    }

    void OnMouseUp () {
		if (!card.usable) return;
		if (card.cardState.spell) {
			card.PlaySpell();
			StartCoroutine(UseSpell());
			return;
		}
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

	IEnumerator UseSpell() {
		float opacity = 1;
		renderer.material.shader = Shader.Find("Transparent/Diffuse");
		transform.Find("OutlineParticle").particleSystem.emissionRate = 0;
		transform.Find("OutlineParticle").particleSystem.Clear();
		transform.Find("SelectParticle").particleSystem.emissionRate = 0;
		transform.Find("SelectParticle").particleSystem.Clear();
		while (opacity > 0.01) {
			opacity -= fadeRate;
			renderer.material.SetColor("_Color", new Color(1, 1, 1, opacity));
			byte byteOpacity = (byte) ((int) 255 * opacity);
			TextMeshPro cost = transform.Find("Cost").GetComponent<TextMeshPro>();
			cost.faceColor = new Color32(255, 255, 255, byteOpacity);
			cost.outlineColor = new Color32(0, 0, 0, byteOpacity);
			yield return new WaitForSeconds(fadeWait);
		}
		Destroy(this.gameObject);
	}
}
