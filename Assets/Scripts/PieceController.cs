using UnityEngine;
using System.Collections;
using TMPro;

public class PieceController : MonoBehaviour {

    public CardState cardState {get; private set;}
    public bool usable {get; private set;}
    
    private GameController game;
    private ParticleSystem outlineParticle;
	private ParticleSystem selectParticle;
    private GameObject attackObj;
    private GameObject healthObj;
	private GameObject timerObj;
    private TextMeshPro attackText;
    private TextMeshPro healthText;

    // temp
    public PlayerController player;
    public Material defaultMaterial;

    public float lastAttackTime {get; private set;}

	public bool attacking = false;
	public string color;
	public Vector3 position;

    public void PlayCard(CardState cardState) {
        this.cardState = cardState;
        lastAttackTime = Time.time;
        if (cardState.speed) lastAttackTime = Time.time - (cardState.time * Util.TimeScaleFactor);
		string attackPath = "materials/piece/attack/attack_" + cardState.color;
		attackObj.renderer.material = (Material) Resources.Load(attackPath, typeof(Material));
		string healthPath = "materials/piece/attack/health_" + cardState.color;
		healthObj.renderer.material = (Material) Resources.Load(healthPath, typeof(Material));
		color = cardState.color;
        UpdateCardHealth();
        UpdateCardAttack();
        gameObject.GetComponent<PiecePlayAnimation>().Animate(cardState);
    }

    public void ReceiveCreatureDamage(int damage, PieceController other) {
        gameObject.GetComponent<PieceDamageAnimation>().Animate(damage, this, other);
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

    public bool Ready() {
        return (cardState != null &&
                (cardState.time * Util.TimeScaleFactor) + lastAttackTime <= Time.time);
    }

	public bool InRange(PieceController other) {
		return (Vector3.Distance(position, other.position) < 65 || cardState.ranged);
	}
	
	public bool CanAttack(PieceController other) {
		if (InRange(other) && other.cardState != null && other.cardState.block) return true;
		foreach (PieceController piece in Util.GetOpponentPieces(player)) {
			if (InRange(piece) && piece.cardState != null && piece.cardState.block && piece != other) {
				return false;
			}
		}
		return InRange(other);
	}

    public void Attack(PieceController other) {
        lastAttackTime = Time.time;
        game.Attack(this, other);
    }
    
    public void ShowOutline() {
        outlineParticle.gameObject.SetActive(true);
    }

    public void HideOutline() {
		outlineParticle.gameObject.SetActive(false);
	}

	public void ShowSelect() {
		selectParticle.gameObject.SetActive(true);
	}
	
	public void HideSelect() {
		selectParticle.gameObject.SetActive(false);
	}

    public void EnableRenderer() {
        renderer.enabled = true;
        attackObj.renderer.enabled = true;
        healthObj.renderer.enabled = true;
		timerObj.SetActive(true);
    }

    public void DisableRenderer() {
        renderer.enabled = false;
        attackObj.renderer.enabled = false;
        healthObj.renderer.enabled = false;
		timerObj.SetActive(false);
        attackText.text = "";
        healthText.text = "";
    }

    public void PieceDeath() {
        cardState = null;
        HideOutline();
		HideSelect();
        gameObject.GetComponent<PieceDeathAnimation>().Animate();
        gameObject.GetComponent<PieceAttackAnimation>().DestroyArrow();
    }

    void Awake() {
        cardState = null;
        usable = Util.CheckPlayer(player);
        game = Camera.main.GetComponent<GameController>();
        outlineParticle = transform.parent.transform.Find("OutlineParticle").gameObject.particleSystem;
		selectParticle = transform.parent.transform.Find("SelectParticle").gameObject.particleSystem;
        attackObj = transform.Find("Attack").gameObject;
        healthObj = transform.Find("Health").gameObject;
		timerObj = transform.Find("Canvas").gameObject;
        attackText = attackObj.transform.Find("Text").GetComponent<TextMeshPro>();
        healthText = healthObj.transform.Find("Text").GetComponent<TextMeshPro>();
		position = transform.position;
        DisableRenderer();
    }

    private void UpdateCardHealth() {
		cardState.health = Mathf.Max(0, cardState.health);
        healthText.text = cardState.health.ToString();
    }

    private void UpdateCardAttack() {
        attackText.text = cardState.attack.ToString();
    }

}
