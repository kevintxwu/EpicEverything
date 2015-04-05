using UnityEngine;
using System.Collections;
using TMPro;

public class PieceDeathAnimation : MonoBehaviour {

    const float fadeRate = 0.1f;
	const float fadeWait = 0.001f;

    private PieceController piece;
    private ParticleSystem debrisParticle;
    private ParticleSystem poofParticle;

    public void Animate() {
        poofParticle.Emit(1000);
        debrisParticle.Emit(10);
        renderer.material.shader = Shader.Find("Transparent/Diffuse");
		StartCoroutine(FadeOut());
    }

    void Start() {
        piece = gameObject.GetComponent<PieceController>();
        debrisParticle = transform.Find("DebrisParticle").particleSystem;
        poofParticle = transform.Find("PoofParticle").particleSystem;
    }

	IEnumerator FadeOut() {
		float opacity = 1;
		while (opacity > 0.01) {
			opacity -= fadeRate;
			renderer.material.SetColor("_Color", new Color(1, 1, 1, opacity));
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
		piece.DisableRenderer();
	}
}
