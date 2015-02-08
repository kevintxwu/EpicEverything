using UnityEngine;
using System.Collections;



public class MoveCardWithMouse : MonoBehaviour {

	private CardController cardController;
	private Vector3 position;

	void Start () {
		cardController = gameObject.GetComponent<CardController>();
		position = transform.position;
	}

	void Update () {
		if (cardController.IsSelected()) {
			Vector3 cameraPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 oldPosition = position;
			position = new Vector3 (cameraPoint.x, Util.CardHeight, cameraPoint.z);
			Vector3 velocity = position - oldPosition;
			transform.position = position;
		}
	}
	
	void OnMouseDown() {
		cardController.PickupCard();
	}
	
	void OnMouseUp () {
		cardController.DropCard();
	}
	
}
