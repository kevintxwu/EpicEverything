using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

	public CardState cardState {get; private set;}
	
	private GameController gameController;
	// temp
	public PlayerController playerController;
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
		Unhighlight();
		this.cardState = cardState;
		lastAttackTime = Time.time;
		if (cardState.speed) {
			lastAttackTime = Time.time - cardState.time;
		}
		gameObject.GetComponent<PlayCardAnimation>().Animate(cardState);
	}
	
	public void ReceiveDamage(int damage, TileController other) {
		gameObject.GetComponent<TileDamageAnimation>().Animate(damage, other);
		if (cardState == null) playerController.ReceiveDamage(damage);
		else {
//			cardState.health -= damage;
			if (cardState.health <= 0) {
				gameObject.GetComponent<TileDeathAnimation>().Animate();
				MinionDeath();
			}
		}
	}
	
	public void Attack(TileController other) {
		lastAttackTime = Time.time;
		gameController.Attack(this, other);
	}
	
	public void MinionDeath() {
		cardState = null;
		renderer.material = defaultMaterial;
	}

	void Remove() {
		//TODO
		return;
	}
	
	void Start () {
		cardState = null;
		gameController = Camera.main.GetComponent<GameController>();
		renderer.material = defaultMaterial;
	}
}
