using UnityEngine;
using System.Collections;

public class CardPlayAnimation : MonoBehaviour {

    public float pieceDropWait;

    private Object piecePrefab;
    private ParticleSystem poofParticle;
    private ParticleSystem highlightParticle;
    private CardController card;
    private float opacity = 1.0f;
    private float fadeRate = 0.04f;
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
        StartCoroutine(DropPiece());
    }

    void FixedUpdate() {
        if (opacity < 0.01) Destroy(gameObject, 1.8f);
        if (fading == true) {
            opacity -= fadeRate;
            renderer.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, opacity));
        }
    }

    void Start() {
        piecePrefab = Resources.Load("DropPiece");
    	card = gameObject.GetComponent<CardController>();
        poofParticle = transform.Find("PoofParticle").particleSystem;
        highlightParticle = transform.Find("HighlightParticle").particleSystem;
    }

    IEnumerator DropPiece() {
        Vector3 position = new Vector3(transform.position.x, Util.CardHeight, transform.position.z + 17);
        GameObject pieceObject = Instantiate(piecePrefab, position, Util.CardRotation) as GameObject;
        pieceObject.renderer.material = card.cardState.pieceMaterial;
        pieceObject.rigidbody.useGravity = true;
        Vector3 force = new Vector3(0, 200, 0);
        pieceObject.rigidbody.AddForceAtPosition(force, transform.position);
        yield return new WaitForSeconds(pieceDropWait);
        Destroy(pieceObject);
        card.PlayPiece();
    }

}
