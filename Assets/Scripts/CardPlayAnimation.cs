using UnityEngine;
using System.Collections;

public class CardPlayAnimation : MonoBehaviour {

    const float fadeRate = 0.04f;
    const float pieceDropWait = 0.69f;

    private Object piecePrefab;
    private ParticleSystem poofParticle;
    private ParticleSystem highlightParticle;
    private CardController card;
    private float opacity;
    private bool fading;

    public void Animate(PieceController piece, int cost) {
        transform.position = new Vector3(
            piece.transform.position.x,
            piece.transform.position.y,
            piece.transform.position.z - 17);
        collider.enabled = false;
        highlightParticle.emissionRate = 0;
        highlightParticle.Clear();
        poofParticle.startSpeed = 30 + Mathf.Min(10, cost) * 2;
        poofParticle.Emit(10000);
        renderer.material.shader = Shader.Find("Transparent/Diffuse");
        fading = true;
        StartCoroutine(DropPiece(piece));
    }

    void FixedUpdate() {
        if (opacity < 0.01) Destroy(gameObject, 1.8f);
        if (fading) {
            opacity -= fadeRate;
            renderer.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, opacity));
        }
    }

    void Start() {
        piecePrefab = Resources.Load("DropPiece");
    	card = gameObject.GetComponent<CardController>();
        poofParticle = transform.Find("PoofParticle").particleSystem;
        highlightParticle = transform.Find("OutlineParticle").particleSystem;
        opacity = 1;
        fading = false;
    }

    IEnumerator DropPiece(PieceController piece) {
        Vector3 position = new Vector3(transform.position.x, Util.CardHeight, transform.position.z + 17);
        GameObject pieceObject = Instantiate(piecePrefab, position, Util.CardRotation) as GameObject;
        pieceObject.renderer.material = card.cardState.pieceMaterial;
        pieceObject.rigidbody.useGravity = true;
        Vector3 force = new Vector3(0, 200, 0);
        pieceObject.rigidbody.AddForceAtPosition(force, transform.position);
        yield return new WaitForSeconds(pieceDropWait);
        Destroy(pieceObject);
        piece.PlayCard(card.cardState);
    }

}
