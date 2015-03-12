using UnityEngine;
using System.Collections;

public class MoveToTransform : MonoBehaviour {

    private float precision = 1;

    public Vector3 position {get; private set;}
    public Quaternion rotation {get; private set;}
    public bool moving;
    
    private float angularVelocity;
    private float duration;
    private Vector3 currentVelocity;

    public void Move(Vector3 position, Quaternion rotation, float duration) {
        moving = true;
        angularVelocity = Quaternion.Angle(rotation, gameObject.transform.rotation) / duration;
        this.position = position;
        this.rotation = rotation;
        this.duration = duration;
        currentVelocity = Vector3.zero;
    }

    void FixedUpdate() {
        if (moving) {
            if ((transform.position - position).magnitude < precision &&
                Quaternion.Angle(transform.rotation, rotation) < precision) {
                moving = false;
                transform.position = position;
                transform.rotation = rotation;
                return;
            }
            transform.position = Vector3.SmoothDamp(
                    transform.position, position, ref currentVelocity, duration);
            float angularStep = angularVelocity * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, rotation, angularStep);
        }
    }

    void Awake() {
        moving = false;
    }
}
