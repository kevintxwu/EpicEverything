using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public static Object prefab = Resources.Load("Arrow");
	public static float y = 2;

	public Vector3 origin;	
	private Vector3 position;
	
	public static ArrowController Create(Vector3 origin) {
		GameObject newObject = Instantiate(prefab) as GameObject;
		ArrowController arrowController = newObject.GetComponent<ArrowController>();
		arrowController.origin = new Vector3(origin.x, y, origin.z);
		return arrowController;
	}

	void Update() {
		Vector3 cameraPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		position = new Vector3 (cameraPoint.x, y, cameraPoint.z);
		// TODO: check if position is in range
		// calculate new transform
		Vector3 diff = position - origin;
		float angle = Mathf.Rad2Deg * Mathf.Atan(diff.x / diff.z);
		transform.rotation = Quaternion.Euler(90, angle, transform.rotation.z);
		transform.position = (position + origin) / 2;
		float magnitude = Mathf.Sqrt(Mathf.Pow(diff.x, 2) + Mathf.Pow(diff.z, 2));
		transform.localScale = new Vector3(transform.localScale.x, magnitude, transform.localScale.z);
	}
}
