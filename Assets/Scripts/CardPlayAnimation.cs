using UnityEngine;
using System.Collections;
using TMPro;

public class CardPlayAnimation : MonoBehaviour {

    const float fadeWait = 0.01f;
    const float fadeRate = 0.04f;
    const float pieceInitWait = 0.25f;
    const float pieceDropWait = 0.69f;

    private Object piecePrefab;
    private ParticleSystem poofParticle;
    private ParticleSystem highlightParticle;
    private CardController card;

    public void Animate(PieceController piece, int cost) {
        transform.position = new Vector3(
            piece.transform.position.x,
            Util.CardHeight,
            piece.transform.position.z - 17);
        collider.enabled = false;
        highlightParticle.emissionRate = 0;
        highlightParticle.Clear();
        poofParticle.startSpeed = 30 + Mathf.Min(10, cost) * 2;
        poofParticle.Emit(10000);
        StartCoroutine(FadeCard());
        StartCoroutine(DropPiece(piece));
    }

    void Start() {
        piecePrefab = Resources.Load("DropPiece");
    	card = gameObject.GetComponent<CardController>();
        poofParticle = transform.Find("PoofParticle").particleSystem;
        highlightParticle = transform.Find("OutlineParticle").particleSystem;
    }

    IEnumerator FadeCard() {
		TextMeshPro attack = transform.Find("Attack").GetComponent<TextMeshPro>();
		TextMeshPro health = transform.Find("Health").GetComponent<TextMeshPro>();
		TextMeshPro cost = transform.Find("Cost").GetComponent<TextMeshPro>();
		TextMeshPro time = transform.Find("Time").GetComponent<TextMeshPro>();
		TextMeshPro name = transform.Find("Name").GetComponent<TextMeshPro>();
		TextMeshPro effect = transform.Find("Effect").GetComponent<TextMeshPro>();
		TextMeshPro type = transform.Find("Type").GetComponent<TextMeshPro>();
        renderer.material.shader = Shader.Find("Transparent/Diffuse");
        renderer.castShadows = false;
        float opacity = 1;
        while (opacity > 0.01) {
            opacity -= fadeRate;
            byte byteOpacity = (byte) ((int) 255 * opacity);
            renderer.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, opacity));
			attack.faceColor = new Color32(0, 0, 0, byteOpacity);
			attack.outlineColor = new Color32(255, 255, 255, byteOpacity);
			health.faceColor = new Color32(0, 0, 0, byteOpacity);
			health.outlineColor = new Color32(255, 255, 255, byteOpacity);
			cost.faceColor = new Color32(0, 0, 0, byteOpacity);
			cost.outlineColor = new Color32(255, 255, 255, byteOpacity);
			time.faceColor = new Color32(0, 0, 0, byteOpacity);
			time.outlineColor = new Color32(255, 255, 255, byteOpacity);
			name.faceColor = new Color32(255, 255, 255, byteOpacity);
			effect.faceColor = new Color32(255, 255, 255, byteOpacity);
			type.faceColor = new Color32(255, 255, 255, byteOpacity);
			yield return new WaitForSeconds(fadeWait);
        }
        Destroy(gameObject, 1.8f);
    }

    IEnumerator DropPiece(PieceController piece) {
        Vector3 position = new Vector3(transform.position.x, Util.CardHeight, transform.position.z + 17);
        GameObject pieceObject = Instantiate(piecePrefab, position, Util.CardRotation) as GameObject;
        pieceObject.layer = 0;
        pieceObject.renderer.material = card.cardState.pieceMaterial;
        yield return new WaitForSeconds(pieceInitWait);
        pieceObject.rigidbody.useGravity = true;
        Vector3 force = new Vector3(0, 200, 0);
        pieceObject.rigidbody.AddForceAtPosition(force, transform.position);
        yield return new WaitForSeconds(pieceDropWait);
        Destroy(pieceObject);
        piece.PlayCard(card.cardState);
    }

}
