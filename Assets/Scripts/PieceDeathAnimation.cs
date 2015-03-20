using UnityEngine;
using System.Collections;

public class PieceDeathAnimation : MonoBehaviour {

    const float fadeRate = 0.04f;

    private PieceController piece;
    private ParticleSystem debrisParticle;
    private ParticleSystem poofParticle;
    private float opacity;
    private bool fading;

    public void Animate() {
        poofParticle.Emit(1000);
        debrisParticle.Emit(10);
        renderer.material.shader = Shader.Find("Transparent/Diffuse");
        fading = true;
    }

    void Start() {
        piece = gameObject.GetComponent<PieceController>();
        debrisParticle = transform.Find("DebrisParticle").particleSystem;
        poofParticle = transform.Find("PoofParticle").particleSystem;
        opacity = 1;
        fading = false;
    }

    void FixedUpdate() {
        if (opacity < 0.01) {
            piece.DisableRenderer();
            opacity = 1;
            fading = false;
        }
        if (fading) {
            opacity -= fadeRate;
            renderer.material.SetColor("_Color", new Color(1, 1, 1, opacity));
        }
    }
}
