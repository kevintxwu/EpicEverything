using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameState {

}

public class GameController : MonoBehaviour {

	public GameState gameState;
	
	private PlayerController p1Controller;
	private PlayerController p2Controller;

	public void Attack(TileController attacker, TileController defender) {
		print("attack");
		CardState attackerCardState = attacker.cardState;
		CardState defenderCardState = defender.cardState;
		if (defenderCardState != null && (AdjacentTile(attacker, defender) || defenderCardState.ranged)) {
			// deal retaliation damage
			attacker.ReceiveDamage(defenderCardState.attack, defender);
		}
		defender.ReceiveDamage(attackerCardState.attack, attacker);
	}
	
	public void DrawCard() {
		p1Controller.DrawCard();
		p2Controller.DrawCard();
	}
	
	public void NewTurn() {
		p1Controller.NewTurn();
		p2Controller.NewTurn();
	}
	

	void Start () {
		p1Controller = GameObject.Find("Player1").GetComponent<PlayerController>();
		p2Controller = GameObject.Find("Player2").GetComponent<PlayerController>();
	}

	
	bool AdjacentTile(TileController a, TileController b) {
		return true;
	}
}
