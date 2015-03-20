using UnityEngine;
using System.Collections;
using TMPro;

public class PieceController : MonoBehaviour {

    public CardState cardState {get; private set;}
    
    private GameController game;
    private ParticleSystem outlineParticle;
    private GameObject attackObj;
    private GameObject healthObj;
    private TextMeshPro attackText;
    private TextMeshPro healthText;

    // temp
    public PlayerController player;
    public Material defaultMaterial;

    public float lastAttackTime {get; private set;}

    public void PlayCard(CardState cardState) {
        this.cardState = cardState;
        lastAttackTime = Time.time;
        if (cardState.speed) lastAttackTime = Time.time - cardState.time;
        attackObj.renderer.material = (Material) Resources.Load("attack_" + cardState.color, typeof(Material));
        healthObj.renderer.material = (Material) Resources.Load("health_" + cardState.color, typeof(Material));
        UpdateCardHealth();
        UpdateCardAttack();
        gameObject.GetComponent<PiecePlayAnimation>().Animate(cardState);
    }

    public void ReceiveCreatureDamage(int damage, PieceController other) {
        gameObject.GetComponent<PieceDamageAnimation>().Animate(damage, other);
        if (cardState == null) player.ReceiveDamage(damage);
        else {
            cardState.health -= damage;
            UpdateCardHealth();
            if (cardState.health <= 0) PieceDeath();
        }
    }

    public void ReceiveSpellDamage(int damage) {
        //TODO
    }

    public bool CanAttack() {
        return (cardState != null &&
                cardState.time + lastAttackTime <= Time.time);
    }

    public void Attack(PieceController other) {
        lastAttackTime = Time.time;
        game.Attack(this, other);
    }
    
    public void ShowOutline() {
        outlineParticle.gameObject.active = true;
    }

    public void HideOutline() {
        outlineParticle.gameObject.active = false;
    }

    public void EnableRenderer() {
        renderer.enabled = true;
        attackObj.renderer.enabled = true;
        healthObj.renderer.enabled = true;
    }

    public void DisableRenderer() {
        renderer.enabled = false;
        attackObj.renderer.enabled = false;
        healthObj.renderer.enabled = false;
        attackText.text = "";
        healthText.text = "";
    }

    public void PieceDeath() {
        cardState = null;
        HideOutline();
        gameObject.GetComponent<PieceDeathAnimation>().Animate();
        gameObject.GetComponent<PieceAttackAnimation>().DestroyArrow();
    }

    void Awake() {
        cardState = null;
        game = Camera.main.GetComponent<GameController>();
        outlineParticle = transform.parent.transform.Find("OutlineParticle").gameObject.particleSystem;
        attackObj = transform.Find("Attack").gameObject;
        healthObj = transform.Find("Health").gameObject;
        attackText = attackObj.transform.Find("Text").GetComponent<TextMeshPro>();
        healthText = healthObj.transform.Find("Text").GetComponent<TextMeshPro>();
        DisableRenderer();
    }

    private void UpdateCardHealth() {
        healthText.text = cardState.health.ToString();
    }

    private void UpdateCardAttack() {
        attackText.text = cardState.attack.ToString();
    }

}
