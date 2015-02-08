using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {
	protected float fDistance = 1;
	protected float fSpeed = 1;
	public GameObject center;

	void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow)) OrbitCenter(false);
		if (Input.GetKey(KeyCode.LeftArrow)) OrbitCenter(true);
		if (Input.GetKey(KeyCode.UpArrow)) OrbitZ(false);
		if (Input.GetKey(KeyCode.DownArrow)) OrbitZ(true);
	}
	
	protected void OrbitCenter(bool bLeft)
	{
		float step = fSpeed * Time.deltaTime;
		float fOrbitCircumfrance = 2F * fDistance * Mathf.PI;
		float fDistanceDegrees = (fSpeed / fOrbitCircumfrance) * 360;
		float fDistanceRadians = (fSpeed / fOrbitCircumfrance) * 2 * Mathf.PI;
		if (bLeft)
			transform.RotateAround(center.transform.position, Vector3.up, -fDistanceRadians);
		else
			transform.RotateAround(center.transform.position, Vector3.up, fDistanceRadians);
	}
	
	protected void OrbitZ(bool bUp)
	{
		float step = fSpeed * Time.deltaTime;
		float fOrbitCircumfrance = 2F * fDistance * Mathf.PI;
		float fDistanceDegrees = (fSpeed / fOrbitCircumfrance) * 360;
		float fDistanceRadians = (fSpeed / fOrbitCircumfrance) * 2 * Mathf.PI;
		if (bUp)
			transform.RotateAround(center.transform.position, Vector3.left, -fDistanceRadians);
		else
			transform.RotateAround(center.transform.position, Vector3.left, fDistanceRadians);
	}
}
