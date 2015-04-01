using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

    public static Object prefab = Resources.Load("Arrow");

    private int snapInterval = 15;

    public Vector3 origin;
    private Vector3 position;
    private GameObject body;
    private GameObject head;

    public static ArrowController Create(Vector3 origin) {
        GameObject newObject = Instantiate(prefab) as GameObject;
        ArrowController arrowController = newObject.GetComponent<ArrowController>();
        arrowController.origin = new Vector3(origin.x, Util.ArrowHeight, origin.z);
        return arrowController;
    }

    public void Snap(bool forward) {
        Vector3 bodyTransformStep;
        if (forward) {
            Vector3 diff = head.transform.position - body.transform.position;
            bodyTransformStep = (diff + 12 * diff.normalized) / snapInterval;
        }
        else bodyTransformStep = (origin - body.transform.position) / snapInterval;
        Vector3 headTransformStep = (head.transform.position - origin).normalized * 2;
        Vector3 bodyScaleStep = new Vector3(0, body.transform.localScale.y, 0) / snapInterval;
        Vector3 headScaleStep = head.transform.localScale / 5;
        StartCoroutine(UpdateSnap(bodyTransformStep, bodyScaleStep, headTransformStep, headScaleStep));
    }

    public void UpdateTransform(Vector3 position) {
        position = new Vector3(position.x, Util.ArrowHeight, position.z);
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

    void Awake() {
        head = transform.Find("Head").gameObject;
        head.transform.position = origin;
        body = transform.Find("Body").gameObject;
    }

    IEnumerator UpdateSnap(Vector3 bodyTransformStep, Vector3 bodyScaleStep, Vector3 headTransformStep, Vector3 headScaleStep) {
        for (int i = 1; i < 6; i++) {
            body.transform.position += bodyTransformStep * i;
            head.transform.position += headTransformStep;
            body.transform.localScale -= bodyScaleStep * i;
            head.transform.localScale -= headScaleStep;
            yield return new WaitForSeconds(Util.ArrowSnapWait); 
        }
        Destroy(gameObject);
    }
}
