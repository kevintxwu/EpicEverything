using UnityEngine;
using System.Collections;

public class HighlightOnMouseover : MonoBehaviour {

	public Vector2 cardSpawnPosition;
	public Vector2 scale;

	private CardController cardController;
	private GameObject expandedCardObject;
	
	void Start() {
		cardController = gameObject.GetComponent<CardController>();
	}

	void OnMouseEnter() {
		if (!cardController.IsSelected()) {
			expandedCardObject = GenerateExpandedCard();
			MakeInvisible();
		}
	}
	
	void OnMouseExit() {
		Destroy(expandedCardObject);
		MakeVisible();
	}
	
	void OnMouseDown() {
		print("mousedown");
		Destroy(expandedCardObject);
		MakeVisible();
	}
	
	GameObject GenerateExpandedCard() {
		Vector3 position = new Vector3(transform.position.x, cardSpawnPosition.x, cardSpawnPosition.y);
		GameObject cardObject = Instantiate(gameObject, position, Quaternion.Euler(90, 0, 0)) as GameObject;
		Destroy(cardObject.collider);
		cardObject.transform.localScale = new Vector3(scale.x, scale.y, 1);
		return cardObject;
	}
	
	void MakeInvisible() {
		renderer.enabled = false;
		foreach (Renderer r in GetComponentsInChildren(typeof(Renderer))) r.enabled = false;
	}
	
	void MakeVisible() {
		renderer.enabled = true;
		foreach (Renderer r in GetComponentsInChildren(typeof(Renderer))) r.enabled = true;
	}
}
