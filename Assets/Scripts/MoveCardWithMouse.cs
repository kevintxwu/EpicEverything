using UnityEngine;
using System.Collections;



public class MoveCardWithMouse : MonoBehaviour {

	public float y;

	private CardController cardController;
	private Vector3 position;

	void Start () {
		print("start");
		cardController = gameObject.GetComponent<CardController>();
		position = transform.position;
	}

	void Update () {
		if (cardController.IsSelected()) {
			// get world position of mouse
			Vector3 cameraPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 oldPosition = position;
			position = new Vector3 (cameraPoint.x, y, cameraPoint.z);
			Vector3 velocity = position - oldPosition;
//			transform.rotation = Quaternion.Euler(
//				Mathf.Clamp(velocity.y * -tilt, -maxTilt, maxTilt),
//				Mathf.Clamp(velocity.x * tilt, -maxTilt, maxTilt),
//				0.0f
//			);
			transform.position = position;
		}
	}
	
	void OnMouseDown() {
		print("mousedown");
		cardController.PickupCard();
	}
	
	void OnMouseUp () {
		cardController.DropCard();
	}
	
}
