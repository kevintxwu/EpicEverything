using UnityEngine;
using System.Collections;

public class PiecePlayAnimation : MonoBehaviour {

    private ParticleSystem dustParticle;
    private ParticleSystem smokeParticle;
    private ParticleSystem crackParticle;

    public void Animate(CardState cardState) {
        int cost = 10;//Mathf.Min(cardState.cost, 10);
        dustParticle.Emit(cost * 100);
        smokeParticle.startSize = cost * 4 + 20;
        smokeParticle.Emit(cost * 10);
        crackParticle.startSize = cost * 10 + 50;
        crackParticle.Emit(1);
        renderer.material = cardState.pieceMaterial;
    }

    void Start() {
        dustParticle = transform.Find("DustParticle").particleSystem;
        smokeParticle = transform.Find("SmokeParticle").particleSystem;
        crackParticle = transform.Find("CrackParticle").particleSystem;
    }

}
