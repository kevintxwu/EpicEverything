using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Util {

    // consts
    public static Quaternion CardRotation = Quaternion.Euler(270, 180, 0);
    public static float ArrowSnapWait = 0.015f;
    public static float ArrowHeight = 50;
    public static float CardSpawnHeight = 10;
    public static float CardHeight = 20;
    public static float RayDepth = 100;
    public static int PieceLayer = 1 << 8;
    public static float TimeScaleFactor = 0.5f;

    public static List<PieceController> p1Pieces = new List<PieceController> {
    	GameObject.Find("Tile1a/Piece").GetComponent<PieceController>(),
		GameObject.Find("Tile1b/Piece").GetComponent<PieceController>(),
		GameObject.Find("Tile1c/Piece").GetComponent<PieceController>(),
		GameObject.Find("Tile1d/Piece").GetComponent<PieceController>(),
		GameObject.Find("Tile1e/Piece").GetComponent<PieceController>()
	};
	public static List<PieceController> p2Pieces = new List<PieceController> {
		GameObject.Find("Tile2a/Piece").GetComponent<PieceController>(),
		GameObject.Find("Tile2b/Piece").GetComponent<PieceController>(),
		GameObject.Find("Tile2c/Piece").GetComponent<PieceController>(),
		GameObject.Find("Tile2d/Piece").GetComponent<PieceController>(),
		GameObject.Find("Tile2e/Piece").GetComponent<PieceController>()
	};

    public static float[] Linspace(float start, float end, int num) {
        float interval = (end - start) / (num - 1);
        float[] linspace = new float[num];
        float curr = start;
        for (int i = 0; i < num; i++) {
            linspace[i] = curr;
            curr += interval;
        }
        return linspace;
    }

    public static Vector3[] Linspace(Vector3 start, Vector3 end, int num) {
        Vector3 interval = (end - start) / (num - 1);
        Vector3[] linspace = new Vector3[num];
        Vector3 curr = start;
        for (int i = 0; i < num; i++) {
            linspace[i] = curr;
            curr += interval;
        }
        return linspace;
    }

    public static void Shuffle<T>(List<T> list) {
        int n = list.Count;
        while (n > 1) {
            int k = (int) (Random.Range(0f, n));
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    
    public static bool CheckPlayer(PlayerController player) {
    	if (player.gameObject.name != "Player1") return false;
    	return true;
    }
    
	public static List<PieceController> GetOpponentPieces(PlayerController player) {
		if (player.gameObject.name == "Player1") return p2Pieces;
		if (player.gameObject.name == "Player2") return p1Pieces;
		return null;
	}
}
