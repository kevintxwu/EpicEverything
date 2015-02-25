using UnityEngine;
using System.Collections;

public class CameraShakeAnimation : MonoBehaviour {

    private float shakeIntensity;
    private float shakeDecay = 0.1f;

    public void Animate(int damage) {
        shakeIntensity = damage * 1f / 5f;
    }

    void Update() {
        if (shakeIntensity >= 0) {
            camera.transform.position = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                camera.transform.position.y,
                Random.Range(-shakeIntensity, shakeIntensity));
            shakeIntensity -= shakeDecay;
        }
    }
}
