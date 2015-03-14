using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PieceTimerAnimation : MonoBehaviour {

    PieceController piece;
    Image timerImage;

    // Use this for initialization
    void Awake () {
        timerImage = transform.Find("Canvas").Find("Timer").GetComponent<Image>();
        piece = gameObject.GetComponent<PieceController>();
    }
    
    // Update is called once per frame
    void Update () {
        if (piece.cardState != null) {
            float remainingWaitTime = piece.lastAttackTime + piece.cardState.time - Time.time;
            if (remainingWaitTime > 0) {
                timerImage.fillAmount = remainingWaitTime / piece.cardState.time;
            } else timerImage.fillAmount = 0;
        }
    }
}
