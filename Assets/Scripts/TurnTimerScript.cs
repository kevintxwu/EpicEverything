using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TurnTimerScript : MonoBehaviour {

    public float initDrawWait;
    public float turnWait;

    private GameController gameController;
    private int numStartingCards = 3;

    void Start () {
        gameController = gameObject.GetComponent<GameController>();
		turnWait *= Util.TimeScaleFactor;
		StartCoroutine(StartGameCountdown());
//        StartCoroutine(DrawInitialCards());
//        StartCoroutine(TurnTimer());
    }

	IEnumerator StartGameCountdown() {
		GameObject startCountdownObject = GameObject.Find("StartCountdown");
		TextMeshPro startCountdownText = startCountdownObject.GetComponent<TextMeshPro>();
		float size = startCountdownText.fontSize;
		for (int i = 3; i > 0; --i) {
			startCountdownText.text = i.ToString();
			for (int j = 0; j < 10; j++) {
				startCountdownText.fontSize = size * j / 10;
				yield return new WaitForSeconds(0.03f);
			}
			yield return new WaitForSeconds(Util.TimeScaleFactor - 0.3f);
		}
		Destroy(startCountdownObject);
		StartCoroutine(DrawInitialCards());
		StartCoroutine(TurnTimer());
	}

    IEnumerator DrawInitialCards() {
        for (int i = 0; i < numStartingCards; i++) {
            gameController.DrawCard();
            yield return new WaitForSeconds(initDrawWait);
        }
    }

    IEnumerator TurnTimer() {
        while (true) {
            yield return new WaitForSeconds(turnWait);
            gameController.NewTurn();
        }
    }


}
