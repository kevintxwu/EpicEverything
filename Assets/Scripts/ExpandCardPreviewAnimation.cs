using UnityEngine;
using System.Collections;

public class ExpandCardPreviewAnimation : MonoBehaviour {

	public float cardSpawnPosition;

	private CardController cardController;
	private GameObject expandedCardObject;
	
	void Start() {
		cardController = gameObject.GetComponent<CardController>();
	}

	void OnMouseEnter() {
		if (!cardController.IsSelected()) {
			ShowExpandedCard();
		}
	}
	
	void OnMouseExit() {
		ShowNormalCard();
	}
	
	void OnMouseDown() {
		ShowNormalCard();
	}
	
	private void ShowExpandedCard() {
		expandedCardObject = GenerateExpandedCard();
		renderer.enabled = false;
		foreach (Renderer r in GetComponentsInChildren(typeof(Renderer))) r.enabled = false;
	}
	
	private void ShowNormalCard() {
		Destroy(expandedCardObject);
		renderer.enabled = true;
		foreach (Renderer r in GetComponentsInChildren(typeof(Renderer))) r.enabled = true;
	}
	
	private GameObject GenerateExpandedCard() {
		Vector3 position = new Vector3(transform.position.x, Util.CardHeight, cardSpawnPosition);
		GameObject cardObject = Instantiate(gameObject, position, Quaternion.Euler(90, 0, 0)) as GameObject;
		Destroy(cardObject.collider);
		cardObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
		return cardObject;
	}
}
