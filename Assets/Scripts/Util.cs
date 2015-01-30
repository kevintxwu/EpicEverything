using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Util {
	
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
}
