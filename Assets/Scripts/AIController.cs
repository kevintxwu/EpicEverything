using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AIController : PlayerController {

    private List<PieceController> pieces;
    private List<PieceController> opponentPieces;

    // private float actionWait = 1;

    public new void PlayCard(CardController card) {
        playerState.hand.Remove(card);
        UpdateGold(playerState.gold - card.cardState.cost);
        UpdateHandPosition();
    }

    protected new void Awake() {
        cardSpawnPosition = new Vector3(-200, 10, 200);
        handAngle = 15;
        pivot = new Vector3(-60, 1, 471);
        length = -350;
        spacing = 6;
        xRotation = 90;
        
        pieces = Util.p2Pieces;
        opponentPieces = Util.p1Pieces;

        Init();
        StartCoroutine(AIAction());
    }

	private bool Randomize() {
		return Random.value < 0.69;
	}

    IEnumerator AIAction() {
        while (true) {
            PieceController playablePiece = null;
            CardController playableCard = null;
            foreach (CardController card in playerState.hand) {
                playablePiece = FindPlayablePiece(card);
                if (playablePiece != null) {
                    playableCard = card;
                    break;
                }
            }
            if (playableCard != null) {
                Vector3 position = new Vector3(
                    playablePiece.transform.position.x,
                    Util.CardHeight,
                    playablePiece.transform.position.z - 17);
                playableCard.PickupCard();
                playableCard.Move(position, playableCard.transform.rotation);
                yield return new WaitForSeconds(3 * playableCard.repositionTime);
                playableCard.PlayCard(playablePiece);
            }
			Util.Shuffle(pieces);
			foreach (PieceController piece in pieces) {
				if (piece.Ready() && Randomize()) {
					PieceController other = opponentPieces[Random.Range(0, opponentPieces.Count)];
					if (piece.CanAttack(other)) {
						Vector3[] intervals = Util.Linspace(piece.transform.position, other.transform.position, 10);
						ArrowController arrow = ArrowController.Create(piece.transform.position);
						for (int i = 1; i < intervals.Length; i++) {
							arrow.UpdateTransform(intervals[i]);
							yield return new WaitForSeconds(0.015f);
						}
						arrow.Snap(other.cardState == null);
						yield return new WaitForSeconds(5 * Util.ArrowSnapWait);
						piece.Attack(other);
						yield return new WaitForSeconds(1);
					}
				}
			}
            yield return new WaitForSeconds(1);
        }
    }

    PieceController FindPlayablePiece(CardController card) {
        foreach (PieceController piece in pieces) {
            if (card.IsPlayable(piece)) return piece;
        }
        return null;
    }

    protected override void UpdateGold(int amount) {
        playerState.gold = Mathf.Min(maxGold, amount);
        goldText.text = playerState.gold.ToString();
    }

//    protected override void AddCardToHand(CardController card) {
//        playerState.hand.Add(card);
//        card.SetPlayerController(this);
//        Destroy(card.gameObject.GetComponent<CardPreviewAnimation>());
//        Destroy(card.gameObject.GetComponent<MoveCardWithMouse>());
//        UpdateHandPosition();
//    }

}
