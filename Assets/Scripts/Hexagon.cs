using UnityEngine;
using System.Collections;

public class Hexagon : MonoBehaviour {

	public float floorLevel;
	public Texture texture;
	public float scale;
	public bool hasCollider;

	void Start () {
		Vector3[] vertices = new Vector3[] {
			new Vector3(-0.5f, floorLevel, -0.5f) * scale,
			new Vector3(-0.5f, floorLevel, 0.5f) * scale,
			new Vector3(0f, floorLevel, 1f) * scale,
			new Vector3(0.5f, floorLevel, 0.5f) * scale,
			new Vector3(0.5f, floorLevel, -0.5f) * scale,
			new Vector3(0f, floorLevel, -1f) * scale
		};
		
		int[] triangles = new int[] {
			1,5,0,
			1,4,5,
			1,2,4,
			2,3,4
		};
		
		Vector2[] uv = new Vector2[] {
			new Vector2(0,0.25f),
			new Vector2(0,0.75f),
			new Vector2(0.5f,1),
			new Vector2(1,0.75f),
			new Vector2(1,0.25f),
			new Vector2(0.5f,0),
		};
				
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;
		mesh.RecalculateNormals();
		
		gameObject.GetComponent<MeshFilter>().mesh = mesh;
		if (hasCollider) gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
//		renderer.material.mainTexture = texture;
	}
}
