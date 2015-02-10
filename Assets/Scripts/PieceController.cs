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

    public void Unhighlight() {
        //transform.Find("Outline").gameObject.SetActive(false);
    }
    
    public void Highlight() {
        //transform.Find("Outline").gameObject.SetActive(true);
    }

    public void PlayCard(CardState cardState) {
        this.cardState = cardState;
        lastAttackTime = Time.time;
        if (cardState.speed) lastAttackTime = Time.time - cardState.time;
        gameObject.GetComponent<PiecePlayAnimation>().Animate(cardState);
    }

    public void ReceiveDamage(int damage, PieceController other) {
        gameObject.GetComponent<PieceDamageAnimation>().Animate(damage, other);
        if (cardState == null) player.ReceiveDamage(damage);
        else {
            // cardState.health -= damage;
            if (cardState.health <= 0) {
                gameObject.GetComponent<PieceDeathAnimation>().Animate();
                MinionDeath();
            }
        }
    }
    
    public void Attack(PieceController other) {
        lastAttackTime = Time.time;
        game.Attack(this, other);
    }
    
    public void MinionDeath() {
        cardState = null;
        renderer.material = defaultMaterial;
    }

    void Remove() {
        //TODO
        return;
    }

    void Awake() {
        cardState = null;
        game = Camera.main.GetComponent<GameController>();
        renderer.material = defaultMaterial;
    }
}
