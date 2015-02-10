using UnityEngine;
using System.Collections;

public class PlayCard : MonoBehaviour {
	public int cost;
	public GameObject piece;
	public GameObject poof;
	public GameObject highlight;
	public GameObject impact;
	public GameObject flash;
	public GameObject cracks;
	private float opacity = 1.0f;
	private float fade = 0.04f;
	private bool fading = false;

	void Start() {
		poof.particleSystem.emissionRate = 0;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyUp (KeyCode.Space)) Play();
		if (opacity < 0.01) {
			opacity = 1.0f;
			fading = false;
			impact.particleSystem.Emit(Mathf.Min(10, cost) * 100);
			flash.particleSystem.startSize = Mathf.Min(10, cost) * 4 + 20;
			flash.particleSystem.Emit(Mathf.Min(10, cost) * 10);
			cracks.particleSystem.startSize = Mathf.Min(10, cost) * 10 + 50;
			cracks.particleSystem.Emit(1);
			Destroy(gameObject);
		} else if (opacity < 0.05) {
			fade = 0.002f;
		}
		if (fading == true) {
			opacity -= fade;
			renderer.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, opacity));
		}
	}

	void Play() {
		Vector3 force = new Vector3 (0, 200, 0);
		Vector3 position = new Vector3 (transform.position.x, 
		                                transform.position.y, 
		                                transform.position.z);
		piece.rigidbody.AddForceAtPosition(force, position);
		highlight.particleSystem.emissionRate = 0;
		highlight.particleSystem.Clear();
		poof.particleSystem.startSpeed = 30 + Mathf.Min(10, cost) * 2;
		poof.particleSystem.Emit(10000);
		piece.rigidbody.useGravity = true;
		renderer.material.shader = Shader.Find("Transparent/Diffuse");
		fading = true;
	}
}
