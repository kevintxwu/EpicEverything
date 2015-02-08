using UnityEngine;
using System.Collections;

public class MoveArrowWithMouse : MonoBehaviour {

	public float y;

	private CardController cardController;
	private Vector3 position;
	
	void Update () {
		Vector3 cameraPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		position = new Vector3 (cameraPoint.x, y, cameraPoint.z);
		transform.position = position;
	}

}
