using UnityEngine;
using System.Collections;

public class PieceController : MonoBehaviour {

    public CardState cardState {get; private set;}
    
    private GameController game;
    // temp
    public PlayerController player;
    // temp
    public Material defaultMaterial;

    public float lastAttackTime {get; private set;}

    public void PlayCard(CardState cardState) {
        this.cardState = cardState;
        lastAttackTime = Time.time;
        if (cardState.speed) lastAttackTime = Time.time - cardState.time;
        gameObject.GetComponent<PiecePlayAnimation>().Animate(cardState);
    }

    public void ReceiveCreatureDamage(int damage, PieceController other) {
        gameObject.GetComponent<PieceDamageAnimation>().Animate(damage, other);
        if (cardState == null) player.ReceiveDamage(damage);
        else {
            cardState.health -= damage;
            if (cardState.health <= 0) PieceDeath();
        }
    }

    public void ReceiveSpellDamage(int damage) {
        //TODO
    }

    public void Attack(PieceController other) {
        lastAttackTime = Time.time;
        game.Attack(this, other);
    }
    
    public void ShowOutline() {
        transform.Find("OutlineParticle").gameObject.active = true;
    }

    public void HideOutline() {
        transform.Find("OutlineParticle").gameObject.active = false;
    }

    void Awake() {
        cardState = null;
        game = Camera.main.GetComponent<GameController>();
        renderer.material = defaultMaterial;
    }

    public void PieceDeath() {
        cardState = null;
        HideOutline();
        gameObject.GetComponent<PieceDeathAnimation>().Animate();
        gameObject.GetComponent<PieceAttackAnimation>().DestroyArrow();
    }

}
