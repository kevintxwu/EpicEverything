using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

    public static Object prefab = Resources.Load("Arrow");
    public static float y = 10;

    private int snapInterval = 15;

    public Vector3 origin;
    public PieceAttackAnimation piece;
    private Vector3 position;
    private GameObject body;
    private GameObject head;
    private bool moving;
    private Vector3 bodyTransformStep;
    private Vector3 headTransformStep;
    private Vector3 bodyScaleStep;
    private Vector3 headScaleStep;
    private int i;

    public static ArrowController Create(Vector3 origin, PieceAttackAnimation piece) {
        GameObject newObject = Instantiate(prefab) as GameObject;
        ArrowController arrowController = newObject.GetComponent<ArrowController>();
        arrowController.origin = new Vector3(origin.x, y, origin.z);
        arrowController.piece = piece;
        return arrowController;
    }

    public void Snap(bool forward) {
        moving = false;
        if (forward) {
            Vector3 diff = head.transform.position - body.transform.position;
            bodyTransformStep = (diff + 12 * diff.normalized) / snapInterval;
        }
        else bodyTransformStep = (origin - body.transform.position) / snapInterval;
        headTransformStep = (head.transform.position - origin).normalized * 2;
        bodyScaleStep = new Vector3(0, body.transform.localScale.y, 0) / snapInterval;
        headScaleStep = head.transform.localScale / 5;
        i = 0;
    }

    void Start() {
        head = transform.Find("Head").gameObject;
        head.transform.position = origin;
        body = transform.Find("Body").gameObject;
        moving = true;
    }

    void FixedUpdate() {
        if (moving) UpdateTransform();
        else UpdateSnap();
    }

    void UpdateTransform() {
        Vector3 cameraPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = new Vector3(cameraPoint.x, y, cameraPoint.z);
        // TODO: limit position to range
        // calculate new transform
        Vector3 diff = position - origin;
        float angle = Mathf.Rad2Deg * Mathf.Atan(diff.x / diff.z);
        if (diff.z <= 0) angle += 180;
        head.transform.position = position - 7 * diff.normalized + new Vector3(0, 0.1f, 0);
        head.transform.rotation = Quaternion.Euler(90, angle, transform.rotation.z);
        if (diff.magnitude > 20) {
            body.renderer.enabled = true;
            body.transform.position = origin + (diff - 12 * diff.normalized) / 2;
            body.transform.rotation = Quaternion.Euler(90, angle, transform.rotation.z);
            body.transform.localScale = new Vector3(
                body.transform.localScale.x, diff.magnitude - 12, body.transform.localScale.z);
        } else body.renderer.enabled = false;
    }

    void UpdateSnap() {
        i++;
        body.transform.position += bodyTransformStep * i;
        head.transform.position += headTransformStep;
        body.transform.localScale -= bodyScaleStep * i;
        head.transform.localScale -= headScaleStep;
        if (i == 5) {
            piece.DestroyArrow();
            piece.Attack();
        }
    }
}
