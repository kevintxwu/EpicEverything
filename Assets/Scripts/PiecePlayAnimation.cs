using UnityEngine;
using System.Collections;
using TMPro;

public class PiecePlayAnimation : MonoBehaviour {

    private PieceController piece;
    private ParticleSystem dustParticle;
    private ParticleSystem smokeParticle;
    private ParticleSystem crackParticle;

	const float fadeWait = 0.002f;
	const float fadeRate = 0.1f;

    public void Animate(CardState cardState) {
        int cost = Mathf.Min(cardState.cost, 10);
        dustParticle.Emit(cost * 100);
        smokeParticle.startSize = cost * 4 + 20;
        smokeParticle.Emit(cost * 10);
        crackParticle.startSize = cost * 10 + 50;
        crackParticle.Emit(1);
        renderer.material = cardState.pieceMaterial;
        piece.EnableRenderer();
		StartCoroutine(FadeInStats());
    }

	IEnumerator FadeInStats() {
		float opacity = 0;
		while (opacity < 0.99) {
			opacity += fadeRate;
			byte byteOpacity = (byte) ((int) 255 * opacity);
			Transform attack = transform.Find("Attack");
			Transform health = transform.Find("Health");
			TextMeshPro attackText = transform.Find("Attack").transform.Find("Text").GetComponent<TextMeshPro>();
			TextMeshPro healthText = transform.Find("Health").transform.Find("Text").GetComponent<TextMeshPro>();
			attack.renderer.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, opacity));
			health.renderer.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, opacity));
			attackText.faceColor = new Color32(255, 255, 255, byteOpacity);
			attackText.outlineColor = new Color32(0, 0, 0, byteOpacity);
			healthText.faceColor = new Color32(255, 255, 255, byteOpacity);
			healthText.outlineColor = new Color32(0, 0, 0, byteOpacity);
			yield return new WaitForSeconds(fadeWait);
		}
	}

    void Start() {
        piece = gameObject.GetComponent<PieceController>();
        dustParticle = transform.Find("DustParticle").particleSystem;
        smokeParticle = transform.Find("SmokeParticle").particleSystem;
        crackParticle = transform.Find("CrackParticle").particleSystem;
    }

}
