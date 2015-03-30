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

			// Slow time by factor of 1.5
			float remainingWaitTime = piece.lastAttackTime + piece.cardState.time * 1.5f - Time.time;

            if (remainingWaitTime > 0) {

				// Slow time by factor of 1.5
				timerImage.fillAmount = remainingWaitTime / (piece.cardState.time * 1.5f);

            } else timerImage.fillAmount = 0;
        }
    }
}
