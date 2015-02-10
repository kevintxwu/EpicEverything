using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnTimerScript : MonoBehaviour {

    public float initDrawWait;
    public float turnWait;

    private GameController gameController;
    private int numStartingCards = 3;

    void Start () {
        gameController = gameObject.GetComponent<GameController>();
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
