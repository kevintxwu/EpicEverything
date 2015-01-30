using UnityEngine;
using System.Collections;

[System.Serializable]
public class CardState {
	public int attack;
	public int health;
	public int time;
	public int cost;
	public bool ranged;
	public bool speed;
	public bool block;
	
	// temporary
	public Material cardMaterial;
	public Material tileMaterial;
}

public class CardController : MonoBehaviour {

	Quaternion baseRotation = Quaternion.Euler(90, 0, 0);
	
	public CardState cardState;
	public float repositionTime;
	public float returnTime;
	
	private PlayerController playerController;
	private MoveToTransform mover;
	private Vector3 position;
	private Quaternion rotation;

	public void DropCard() {
		playerController.DropCard(this);
	}

	public void PickupCard() {
		position = mover.position;
		rotation = mover.rotation;
		transform.rotation = baseRotation;
		playerController.PickupCard(this);
	}
	
	public void MoveInHand(Vector3 position, Quaternion rotation) {
		MoveToTransform(position, rotation, repositionTime);
	}

	public void MoveInHandOnDrop(Vector3 position, Quaternion rotation) {
		this.position = position;
		this.rotation = rotation;
	}

	public void ReturnToHand() {
		MoveToTransform(position, rotation, returnTime);
	}

	public bool IsSelected() {
		if (playerController == null) return false;
		return playerController.selectedCard == this;
	}

	public void SetPlayerController(PlayerController playerController) {
		this.playerController = playerController;
	}

	void Awake() {
		renderer.material = cardState.cardMaterial;
		mover = gameObject.GetComponent<MoveToTransform>();
		transform.Find("Attack").GetComponent<TextMesh>().text = cardState.attack.ToString();
		transform.Find("Health").GetComponent<TextMesh>().text = cardState.health.ToString();
		transform.Find("Cost").GetComponent<TextMesh>().text = cardState.cost.ToString();
		transform.Find("Time").GetComponent<TextMesh>().text = cardState.time.ToString();
	}
	
	void MoveToTransform(Vector3 position, Quaternion rotation, float time) {
		mover.Move(position, rotation, time);
	}
}
